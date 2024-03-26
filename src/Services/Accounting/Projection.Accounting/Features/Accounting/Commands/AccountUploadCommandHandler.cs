using CsvHelper;
using Google.Cloud.Storage.V1;
using MediatR;
using Projection.Accounting.Core.Entities;
using Projection.Accounting.Features.Accounting.IntegrationEvents;
using Projection.Accounting.Features.Accounting.Requests;
using Projection.BuildingBlocks.IntegrationEventLogEF;
using Projection.Common.IntegrationService;
using Projection.ServiceDefaults.Services;
using System.Globalization;
using System.Text;
using static Projection.Accounting.Features.Accounting.Commands.AccountUploadCommandHandler;

namespace Projection.Accounting.Features.Accounting.Commands;

public class AccountUploadCommandHandler : IRequestHandler<AccountUploadCommand, bool>
{
    private readonly IAccountRepository _repository;
    private readonly ILogger<AccountUploadCommandHandler> _logger;
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;
    private readonly IApiIntegrationEventService<IntegrationEventLogContext> _apiIntegrationEventService;

    public AccountUploadCommandHandler(IAccountRepository repository, ILogger<AccountUploadCommandHandler> logger, IIdentityService identityService, IMediator mediator, IApiIntegrationEventService<IntegrationEventLogContext> apiIntegrationEventService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _apiIntegrationEventService = apiIntegrationEventService ?? throw new ArgumentNullException(nameof(apiIntegrationEventService));
    }

    public async Task<bool> Handle(AccountUploadCommand request, CancellationToken cancellationToken)
    {
        byte[] bytes = System.Convert.FromBase64String(request.AccountsFile);

        string bucketName = "projection360";
        string objectName = $"accounts_{DateTime.UtcNow.Ticks}.csv";

        var storage = StorageClient.Create();
        using var fileStream = new MemoryStream(bytes);
        var result = await storage.UploadObjectAsync(bucketName, objectName, null, fileStream);

        if(result is not null)
        {
            _logger.LogInformation($"File uploaded to {bucketName}/{objectName}");
            var loggedInUserId = _identityService.GetUserIdentity();

            var accountsFileUploadedEvent = new AccountsFileUploadedIntegrationEvent()
            {
                FileName = objectName,
                BucketName = bucketName,
                UploadedBy = loggedInUserId,
                //FileObject = result
            };

            await _apiIntegrationEventService.AddAndSaveEventAsync(accountsFileUploadedEvent);

            return true;
        }
        else
        {
            _logger.LogError($"File upload failed to {bucketName}/{objectName}");
            return false;
        }
    }

    public record RangeResult
    {
        public int Created { get; set; }
        public int Failed { get; set; }
        public int Total { get; set; }
    }
}
