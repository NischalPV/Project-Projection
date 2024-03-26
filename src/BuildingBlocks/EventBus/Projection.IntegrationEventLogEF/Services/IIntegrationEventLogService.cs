namespace Projection.BuildingBlocks.IntegrationEventLogEF.Services;

public interface IIntegrationEventLogService
{
    Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);
    Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);
    Task MarkEventAsPublishedAsync(Guid eventId);
    Task MarkEventAsInProgressAsync(Guid eventId);
    Task MarkEventAsFailedAsync(Guid eventId);
    Task<IntegrationEventLogEntry> GetEventLogByIdAsync(Guid eventId);
}
