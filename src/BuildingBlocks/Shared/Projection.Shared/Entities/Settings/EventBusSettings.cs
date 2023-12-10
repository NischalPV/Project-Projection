namespace Projection.BuildingBlocks.Shared.Entities.Settings;

public class EventBusSettings
{
    public string ConnectionString { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int RetryCount { get; set; }
    public string SubscriptionClientName { get; set; }
}
