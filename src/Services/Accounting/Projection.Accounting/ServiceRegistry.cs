using Projection.Accounting.Idempotency;
using Projection.BuildingBlocks.Shared.Idempotency;

namespace Projection.Accounting;

public static class ServiceRegistry
{
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {

        services.AddScoped(typeof(IBaseEntityAsyncRepository<,,>), typeof(BaseEntityEfRepository<,,>));
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IRequestManager, RequestManager>();
        return services;
    }
}
