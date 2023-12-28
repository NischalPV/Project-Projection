using MediatR;
using Projection.Accounting.Core.Aggregates;
using Projection.Accounting.Core.Entities;
using Projection.Accounting.Features.Accounting.IntegrationEvents;
using Projection.Accounting.Infrastructure.Data;
using Projection.BuildingBlocks.EventBus.Events;
using Projection.BuildingBlocks.IntegrationEventLogEF;
using Projection.Common.IntegrationService;
using Projection.ServiceDefaults.Services;

namespace Projection.Accounting.Features.Accounting.Commands;

public class AccountCreateCommandHandler : IRequestHandler<AccountCreateCommand, Account>
{
    private readonly IAccountRepository _repository;
    private readonly ILogger<AccountCreateCommandHandler> _logger;
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;
    private readonly IApiIntegrationEventService<IntegrationEventLogContext> _apiIntegrationEventService;

    public AccountCreateCommandHandler(IAccountRepository repository, ILogger<AccountCreateCommandHandler> logger, IIdentityService identityService, IMediator mediator, IApiIntegrationEventService<IntegrationEventLogContext> apiIntegrationEventService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _apiIntegrationEventService = apiIntegrationEventService ?? throw new ArgumentNullException(nameof(apiIntegrationEventService));
    }

    public async Task<Account> Handle(AccountCreateCommand request, CancellationToken cancellationToken)
    {
        var loggedInUserId = _identityService.GetUserIdentity();

        var accountCreatedEvent = new AccountCreatedIntegrationEvent()
        {
            Name = request.Name,
            AccountNumber = request.AccountNumber,
            GSTNumber = request.GSTNumber,
            PANNumber = request.PANNumber,
            Balance = request.Balance,
            CreatedBy = loggedInUserId,
            Description = request.Description,
            CurrencyId = request.CurrencyId,
            StatusId = 1
        };
        await _apiIntegrationEventService.AddAndSaveEventAsync(accountCreatedEvent);

        var account = new Account()
        {
            AccountNumber = request.AccountNumber,
            PANNumber = request.PANNumber,
            GSTNumber = request.GSTNumber,
            CreatedBy = _identityService.GetUserIdentity(),
            Balance = request.Balance,
            CurrencyId = request.CurrencyId,
            Name= request.Name,
            Description = request.Description,
            Contacts = request.Contacts.ToList()
        };

        _logger.LogInformation("----- Creating Account - Account: {@Account}", account);

        var newAccount = await _repository.AddAsync(account, false);

        var result = await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return newAccount;
    }
}
