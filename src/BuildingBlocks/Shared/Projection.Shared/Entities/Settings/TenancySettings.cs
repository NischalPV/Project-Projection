namespace Projection.BuildingBlocks.Shared.Entities.Settings;

public class TenancySettings
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string DefaultConnection { get; set; }
    public string AccountingDbConnection { get; set; }
    public EventBusSettings EventBusSettings { get; set; }
}
