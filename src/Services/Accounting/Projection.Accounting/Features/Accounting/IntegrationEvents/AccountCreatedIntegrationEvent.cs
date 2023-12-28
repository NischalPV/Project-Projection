using Projection.BuildingBlocks.EventBus.Events;

namespace Projection.Accounting.Features.Accounting.IntegrationEvents;

public record AccountCreatedIntegrationEvent: IntegrationEvent
{
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public string Description { get; set; }
    public string CurrencyId { get; set; }
    public double Balance { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string PANNumber { get; set; }
    public string GSTNumber { get; set; }
    public int StatusId { get; set; }

}
