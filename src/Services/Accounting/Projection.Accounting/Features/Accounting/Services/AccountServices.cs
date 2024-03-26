using MediatR;
using Projection.Accounting.Features.Accounting.Interfaces;
using Projection.ServiceDefaults.Services;

namespace Projection.Accounting.Features.Accounting.Services;

public class AccountServices(IMediator mediator, IIdentityService identityService, ILogger<AccountServices> logger, IAccountRepository repository)
{
    public IMediator Mediator { get; set; } = mediator;
    public IIdentityService IdentityService { get; } = identityService;
    public ILogger<AccountServices> Logger { get; } = logger;
    public IAccountRepository Repository { get; } = repository;
}
