using Microsoft.FluentUI.AspNetCore.Components;
using Projection.BuildingBlocks.Shared.Entities;

namespace Projection.UI.Web.Models;

public record Account
{
    public string AccountNumber { get; set; }
    public string GSTNumber { get; set; }
    public string PANNumber { get; set; }
    public double Balance { get; set; }
    public Currency Currency { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string ModifiedBy { get; set; }
    public Status Status { get; set; }
    public string CreatedByUser { get; set; }
    public string CurrencySymbol => Currency.AlphabeticCode;
    public string StatusName => Status.Name;
    public string BalanceWithCurrency => $"{Currency.AlphabeticCode} {Balance}";

    public Appearance Appearance => Status.Id == 8 ? Appearance.Lightweight : Appearance.Neutral;
}
