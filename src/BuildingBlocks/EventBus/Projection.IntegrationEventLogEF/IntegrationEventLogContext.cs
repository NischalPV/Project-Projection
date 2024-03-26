using Microsoft.EntityFrameworkCore;
using Projection.BuildingBlocks.Shared.Constants;
using Projection.Common.DataService.Contexts;

namespace Projection.BuildingBlocks.IntegrationEventLogEF;

public class IntegrationEventLogContext : BaseDbContext
{
    public IntegrationEventLogContext(DbContextOptions<IntegrationEventLogContext> options) : base(options)
    {
    }

    public DbSet<IntegrationEventLogEntry> IntegrationEventLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(Schema.INTEGRATION_SCHEMA);
        builder.Entity<IntegrationEventLogEntry>(ConfigureIntegrationEventLogEntry);

        base.OnModelCreating(builder);
    }

    void ConfigureIntegrationEventLogEntry(EntityTypeBuilder<IntegrationEventLogEntry> builder)
    {
        builder.ToTable("IntegrationEventLogs");

        builder.HasKey(e => e.EventId);

        builder.Property(e => e.EventId)
            .IsRequired();

        builder.Property(e => e.Content)
            .IsRequired();

        builder.Property(e => e.CreationTime)
            .IsRequired();

        builder.Property(e => e.State)
            .IsRequired();

        builder.Property(e => e.TimesSent)
            .IsRequired();

        builder.Property(e => e.EventTypeName)
            .IsRequired();

        builder.Property(e => e.TransactionId);

    }
}