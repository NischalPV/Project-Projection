﻿using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Projection.UI.Web.Infrastructure;
using Projection.UI.Web.Services;

namespace Projection.UI.Web.Components.Layout;

public partial class SiteSettingsPanel
{
    private string? _status;
    private bool _popVisible;

    [Inject]
    public ILogger<SiteSettingsPanel> Logger { get; set; } = default!;

    [Inject]
    public CacheStorageAccessor CacheStorageAccessor { get; set; } = default!;


    public DesignThemeModes Mode { get; set; }

    public OfficeColor? OfficeColor { get; set; }

    public bool Direction { get; set; } = true;

    private IEnumerable<DesignThemeModes> AllModes => Enum.GetValues<DesignThemeModes>();

    private IEnumerable<OfficeColor?> AllOfficeColors
    {
        get
        {
            return Enum.GetValues<OfficeColor>().Select(i => (OfficeColor?)i).Union(new[] { (OfficeColor?)null });
        }
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
