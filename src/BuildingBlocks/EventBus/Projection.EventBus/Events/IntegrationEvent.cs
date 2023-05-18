namespace Projection.BuildingBlocks.EventBus.Events;
public abstract class IntegrationEvent
{
    [JsonProperty]
    public Guid Id { get; }

    [JsonProperty]
    public DateTime CreationDate { get; }

    #region ctors
    [JsonConstructor]
    public IntegrationEvent(Guid id, DateTime creationDate)
    {
        Id = id;
        CreationDate = creationDate;
    }
    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }
    #endregion
}