using Projection.Common.DataService.Contexts;
using Projection.BuildingBlocks.EventBus.Events;

namespace Projection.Common.IntegrationService;

public interface IApiIntegrationEventService<TContext> where TContext : BaseDbContext
{
    Task AddAndSaveEventAsync(IntegrationEvent evt);
    Task PublishEventsThroughEventBusAsync(Guid transactionId, string appName);
}
