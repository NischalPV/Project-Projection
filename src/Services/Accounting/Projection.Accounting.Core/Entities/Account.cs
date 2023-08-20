using Projection.Common.BaseEntities;

namespace Projection.Accounting.Core.Entities;

public record Account : BaseEntity<string>
{
    public string AccountNumber { get; set; }
    public string GSTNumber { get; set; }
    public string PANNumber { get; set; }
    public double Balance { get; set; }
}

