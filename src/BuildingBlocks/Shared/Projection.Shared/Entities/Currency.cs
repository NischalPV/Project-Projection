using System.ComponentModel.DataAnnotations.Schema;

namespace Projection.BuildingBlocks.Shared.Entities;

public record Currency: BaseEntity<string>
{
    public string Symbol { get; set; }
    public int? NumericCode { get; set; }
    public string AlphabeticCode { get; set; }

    [ForeignKey(nameof(Country))]
    public string CountryId { get; set; }

    public virtual Country Country { get; set; }
    
}