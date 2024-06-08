using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Extensions;
using Projection.UI.Web.Infrastructure;
using Projection.UI.Web.Services;

namespace Projection.UI.Web.Components.Layout;

public partial class SiteSettingsPanel
{
    private string? _status;
    private bool _popVisible;
    private bool _ltr = true;
    private FluentDesignTheme? _theme;

    [Inject]
    public ILogger<SiteSettingsPanel> Logger { get; set; } = default!;

    [Inject]
    public CacheStorageAccessor CacheStorageAccessor { get; set; } = default!;

    [Inject]
    public required GlobalState GlobalState { get; set; }

    public DesignThemeModes Mode { get; set; }

    public OfficeColor? OfficeColor { get; set; }

    public LocalizationDirection? Direction { get; set; }

    private IEnumerable<DesignThemeModes> AllModes => Enum.GetValues<DesignThemeModes>();

    private IEnumerable<OfficeColor?> AllOfficeColors
    {
        get
        {
            return Enum.GetValues<OfficeColor>().Select(i => (OfficeColor?)i).Union(new[] { (OfficeColor?)null });
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            Direction = GlobalState.Dir;
            _ltr = !Direction.HasValue || Direction.Value == LocalizationDirection.LeftToRight;
        }
    }

    protected void HandleDirectionChanged(bool isLeftToRight)
    {

        _ltr = isLeftToRight;
        Direction = isLeftToRight ? LocalizationDirection.LeftToRight : LocalizationDirection.RightToLeft;
    }

    private async Task ResetSiteAsync()
    {
        var msg = "Site settings reset and cache cleared!";

        await CacheStorageAccessor.RemoveAllAsync();
        _theme?.ClearLocalStorageAsync();

        Logger.LogInformation(msg);
        _status = msg;

        OfficeColor = OfficeColorUtilities.GetRandom();
        Mode = DesignThemeModes.System;
    }

    private static string? GetCustomColor(OfficeColor? color)
    {
        return color switch
        {
            null => OfficeColorUtilities.GetRandom(true).ToAttributeValue(),
            Microsoft.FluentUI.AspNetCore.Components.OfficeColor.Default => "#036ac4",
            _ => color.ToAttributeValue(),
        };

    }

    [Parameter]
    public SiteSettingsPanelParameters Content { get; set; } = default!;

    private async Task RemoveAllCache()
    {
        await CacheStorageAccessor.RemoveAllAsync();
        Logger.LogInformation("Cache cleared!");

        _status = "Cache cleared!";
    }

    [CascadingParameter]
    public HttpContext? HttpContext { get; set; }


    [Inject]
    private LogOutService? LogOutService { get; set; }

    private Task LogOutAsync()
    => LogOutService!.LogOutAsync(HttpContext!);
}
