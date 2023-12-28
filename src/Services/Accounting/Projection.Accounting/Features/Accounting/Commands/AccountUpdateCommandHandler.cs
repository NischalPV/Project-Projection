using MediatR;
using Projection.Accounting.Core.Entities;
using Projection.Accounting.Features.Accounting.IntegrationEvents;
using Projection.BuildingBlocks.IntegrationEventLogEF;
using Projection.Common.IntegrationService;
using Projection.ServiceDefaults.Services;

namespace Projection.Accounting.Features.Accounting.Commands;

public class AccountUpdateCommandHandler : IRequestHandler<AccountUpdateCommand, Account>
{
    private readonly IAccountRepository _repository;
    private readonly ILogger<AccountUpdateCommandHandler> _logger;
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;
    private readonly IApiIntegrationEventService<IntegrationEventLogContext> _apiIntegrationEventService;

    public AccountUpdateCommandHandler(IAccountRepository repository, ILogger<AccountUpdateCommandHandler> logger, IIdentityService identityService, IMediator mediator, IApiIntegrationEventService<IntegrationEventLogContext> apiIntegrationEventService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _apiIntegrationEventService = apiIntegrationEventService ?? throw new ArgumentNullException(nameof(apiIntegrationEventService));
    }

    public async Task<Account> Handle(AccountUpdateCommand request, CancellationToken cancellationToken)
    {
        var loggedInUserId = _identityService.GetUserIdentity();

        var accountUpdatedEvent = new AccountUpdatedIntegrationEvent()
        {
            Name = request.Name,
            GSTNumber = request.GSTNumber,
            PANNumber = request.PANNumber,
            ModifiedBy = loggedInUserId,
            Description = request.Description,
            Contacts = request.Contacts.ToList()
        };
        await _apiIntegrationEventService.AddAndSaveEventAsync(accountUpdatedEvent);

        var dbAccount = await _repository.GetByIdAsync(request.Id);

        dbAccount.Name = request.Name;
        dbAccount.GSTNumber = request.GSTNumber;
        dbAccount.PANNumber = request.PANNumber;
        dbAccount.ModifiedBy = loggedInUserId;
        dbAccount.Description = request.Description;
        dbAccount.Contacts = request.Contacts.ToList();

        _logger.LogInformation("----- Updating Account - Account: {@Account}", dbAccount);

        var account = await _repository.UpdateAsync(dbAccount, false);

        var result = await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return account;
    }
}
