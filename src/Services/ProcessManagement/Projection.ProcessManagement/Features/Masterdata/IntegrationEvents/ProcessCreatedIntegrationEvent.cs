using Projection.BuildingBlocks.EventBus.Events;

namespace Projection.ProcessManagement.Features.Masterdata.IntegrationEvents;

public record ProcessCreatedIntegrationEvent : IntegrationEvent
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public bool IsMandatory { get; set; } = false;
    public bool IsAutomated { get; set; } = false;
    public Dictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();
    public int StatusId { get; set; }
}
