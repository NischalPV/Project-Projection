using MediatR;
using Projection.Accounting.Features.Accounting.IntegrationEvents;
using Projection.Accounting.Features.Accounting.Specifications;
using Projection.BuildingBlocks.IntegrationEventLogEF;
using Projection.Common.IntegrationService;
using Projection.ServiceDefaults.Services;

namespace Projection.Accounting.Features.Accounting.Commands;

public class AccountTerminateCommandHandler : IRequestHandler<AccountTerminateCommand, string>
{
    private readonly IAccountRepository _repository;
    private readonly ILogger<AccountUpdateCommandHandler> _logger;
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;
    private readonly IApiIntegrationEventService<IntegrationEventLogContext> _apiIntegrationEventService;

    public AccountTerminateCommandHandler(IAccountRepository repository, ILogger<AccountUpdateCommandHandler> logger, IIdentityService identityService, IMediator mediator, IApiIntegrationEventService<IntegrationEventLogContext> apiIntegrationEventService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _apiIntegrationEventService = apiIntegrationEventService ?? throw new ArgumentNullException(nameof(apiIntegrationEventService));
    }

    public async Task<string> Handle(AccountTerminateCommand request, CancellationToken cancellationToken)
    {
        var loggedInUserId = _identityService.GetUserIdentity();

        var dbAccount = await _repository.GetByIdAsync(new AccountWithTransactionsSpecification(request.Id));

        string result = String.Empty;


        if (dbAccount == null)
        {
            result = "Account not found";
        }

        if (dbAccount != null && dbAccount.Balance != 0)
        {
            result = "Account balance is not zero";
        }

        if (dbAccount != null && dbAccount.StatusId == 8)
        {
            result = "Account is already terminated";
        }

        if (result == String.Empty)
        {
            var accountTerminatedEvent = new AccountTerminatedIntegrationEvent()
            {
                AccountNumber = dbAccount.AccountNumber
            };
            await _apiIntegrationEventService.AddAndSaveEventAsync(accountTerminatedEvent);


            dbAccount.Terminate(loggedInUserId);


            _logger.LogInformation("----- Terminating Account - Account: {@Account}", dbAccount);

            var account = await _repository.UpdateAsync(dbAccount, false);

            var saveResult = await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (saveResult)
                result = String.Empty;
            else
                result = "Account termination failed";
        }

        return result;

    }
}
