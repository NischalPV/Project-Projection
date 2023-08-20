namespace Projection.Common.BaseEntities;

public class EventBusSettings
{
    public string ConnectionString { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int RetryCount { get; set; }
    public string SubscriptionClientName { get; set; }
}

public class TenancySettings
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string DefaultConnection { get; set; }
    public string AccountingDbConnection { get; set; }
    public EventBusSettings EventBusSettings { get; set; }
}
