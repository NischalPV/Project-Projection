using Projection.BuildingBlocks.Shared.Idempotency;

namespace Projection.ProcessManagement;

public static class ServiceRegistry
{
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {

        services.AddScoped(typeof(IBaseEntityAsyncRepository<,,>), typeof(BaseEntityEfRepository<,,>));
        return services;
    }
}
