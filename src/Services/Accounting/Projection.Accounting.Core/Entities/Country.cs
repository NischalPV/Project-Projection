using Projection.Common.BaseEntities;

namespace Projection.Accounting.Core.Entities;

public record Country: BaseEntity<string>
{
    public string Code { get; set; }
}