namespace Projection.BuildingBlocks.IntegrationEventLogEF.Extensions;

public static class IntegrationLogExtensions
{
    public static void UseIntegrationEventLogs(this ModelBuilder builder)
    {
        builder.Entity<IntegrationEventLogEntry>(builder =>
        {
            builder.ToTable("IntegrationEventLogs");

            builder.HasKey(e => e.EventId);
        });
    }
}
