
using CsvHelper;
using Google.Cloud.Storage.V1;
using MassTransit;
using Projection.Accounting.Core.Entities;
using Projection.Accounting.Features.Accounting.IntegrationEvents;
using Projection.Accounting.Features.Accounting.Requests;
using Projection.Accounting.Features.Accounting.Specifications;
using Projection.BuildingBlocks.EventBus.Events;
using Projection.BuildingBlocks.IntegrationEventLogEF;
using Projection.Common.BaseEntities;
using Projection.Common.IntegrationService;
using System.Globalization;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Projection.Accounting.Features.Accounting.BackgroundTasks;

public class ImportAccounts : BaseConsumer<IntegrationEvent>
{
    private readonly ILogger<ImportAccounts> _logger;
    private readonly IAccountRepository _repository;
    private readonly IApiIntegrationEventService<IntegrationEventLogContext> _apiIntegrationEventService;

    string bucketName = "projection360";



    public ImportAccounts(ILogger<ImportAccounts> logger, IAccountRepository repository, IApiIntegrationEventService<IntegrationEventLogContext> apiIntegrationEventService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _apiIntegrationEventService = apiIntegrationEventService ?? throw new ArgumentNullException(nameof(apiIntegrationEventService));
    }

    public override async Task Consume(ConsumeContext<IntegrationEvent> context)
    {
        _logger.LogInformation($"Event received: {context.Message.Id}");

        var eventLog = await _apiIntegrationEventService.GetIntegrationEventLogEntryAsync(context.Message.Id);

        var accountsFileUploadedEvent = JsonSerializer.Deserialize<AccountsFileUploadedIntegrationEvent>(eventLog.Content);

        var storage = StorageClient.Create();
        using var fileStream = new MemoryStream();

        var result = await storage.DownloadObjectAsync(bucketName, accountsFileUploadedEvent.FileName, fileStream);
        byte[] bytes = fileStream.ToArray();
        var fileContent = Encoding.UTF8.GetString(bytes);

        _logger.LogInformation($"csv File received: size - {bytes.Count()}");

        using var csv = new CsvReader(new StringReader(fileContent), CultureInfo.InvariantCulture);

        var records = csv.GetRecords<CreateAccountRequest>().ToList();

        var currencyCodes = records.Select(x => x.Currency).Distinct().ToList();

        Dictionary<string, Currency> currencies = new Dictionary<string, Currency>();

        foreach (var currencyCode in currencyCodes)
        {
            var currency = await _repository.GetCurrencyAsync(currencyCode);

            currencies[currencyCode] = currency;
        }

        var gstNumbers = records.Select(x => x.GSTNumber).Distinct().ToList();
        var existingAccounts = (await _repository.ListAllAsync(new AccountWithGSTNumbersSpecification(gstNumbers))).Select(a => a.GSTNumber.ToUpper()).ToList();

        records = records.Except(records.Where(r => existingAccounts.Contains(r.GSTNumber.ToLower()))).ToList();

        List<Account> accounts = new List<Account>();

        foreach (var record in records)
        {
            var account = new Account()
            {
                PANNumber = record.PANNumber.ToUpper(),
                GSTNumber = record.GSTNumber.ToUpper(),
                CreatedBy = accountsFileUploadedEvent.UploadedBy,
                Balance = record.Balance,
                CurrencyId = currencies[record.Currency].Id,
                Name = record.Name,
                Description = record.Description,
                StatusId = (int) StatusEnum.Inactive,
                Contacts = new List<PointOfContact>()
            };


            accounts.Add(account);
        }

        _logger.LogInformation($"Importing the Accounts: count - {accounts.Count}");
        var saveResult = await _repository.AddRangeAsync(accounts, doSave: true);
        _logger.LogInformation($"Accounts Imported: count - {saveResult}/{records.Count}");


    }
}
