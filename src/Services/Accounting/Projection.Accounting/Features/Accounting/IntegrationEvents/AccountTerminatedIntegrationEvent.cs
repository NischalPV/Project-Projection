using Projection.BuildingBlocks.EventBus.Events;

namespace Projection.Accounting.Features.Accounting.IntegrationEvents;

public record AccountTerminatedIntegrationEvent: IntegrationEvent
{
    public string AccountNumber { get; set; }
}
