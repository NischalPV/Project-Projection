using Projection.Accounting.Core.Entities;
using Projection.BuildingBlocks.EventBus.Events;

namespace Projection.Accounting.Features.Accounting.IntegrationEvents;

public record AccountUpdatedIntegrationEvent: IntegrationEvent
{
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public string Description { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    public string PANNumber { get; set; }
    public string GSTNumber { get; set; }
    public int StatusId { get; set; }
    public List<PointOfContact> Contacts { get; set; }
}
