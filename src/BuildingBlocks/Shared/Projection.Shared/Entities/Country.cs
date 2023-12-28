namespace Projection.BuildingBlocks.Shared.Entities;

public record Country: BaseEntity<string>
{
    public string Code { get; set; }
}