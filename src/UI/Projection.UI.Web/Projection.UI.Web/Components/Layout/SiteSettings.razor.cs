using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Projection.UI.Web.Services;

namespace Projection.UI.Web.Components.Layout;

public partial class SiteSettings
{
    private IDialogReference? _dialog;

    private async Task OpenSiteSettingsAsync()
    {
        siteSettingsPanelParameters.ShowLogOutButton = _showLogOutButton;

        _dialog = await DialogService.ShowPanelAsync<SiteSettingsPanel>(siteSettingsPanelParameters, new DialogParameters<SiteSettingsPanelParameters>()
        {
            ShowTitle = true,
            Title = $"Hello {_userName}",
            Alignment = HorizontalAlignment.Right,
            PrimaryAction = "OK",
            SecondaryAction = null,
            ShowDismiss = true,
            Content = siteSettingsPanelParameters
        });

        DialogResult result = await _dialog.Result;
    }


    [Parameter]
    public string UserName { get; set; } = String.Empty;

    [Parameter]
    public bool ShowLogOutButton { get; set; } = false;

    [CascadingParameter]
    public HttpContext? HttpContext { get; set; } = default;

    private SiteSettingsPanelParameters siteSettingsPanelParameters = new();

    private string _userName => UserName != String.Empty ? UserName : "User";
    private bool _showLogOutButton => ShowLogOutButton.Equals(true) ? true : false;

   


}

public class SiteSettingsPanelParameters
{
    public bool ShowLogOutButton { get; set; }

}
