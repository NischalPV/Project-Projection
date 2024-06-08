using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Projection.GlobalConstants;
using Projection.UI.Web.Models;

namespace Projection.UI.Web.Components.Pages.Accounting;

public partial class Accounts
{
    private IQueryable<Account>? accounts;
    PaginationState pagination = new PaginationState { ItemsPerPage = ResultView.DefaultPageSize };
    string accountFilter = string.Empty;
    FluentDataGrid<Account>? grid;

    IQueryable<Account>? FilteredItems => accounts?.Where(x => x.AccountNumber.Contains(accountFilter, StringComparison.CurrentCultureIgnoreCase));

    protected override async Task OnInitializedAsync()
    {
        grid?.SetLoadingState(true);

        var iAccounts = await AccountingService.GetAccounts(pagination.CurrentPageIndex + 1, ResultView.DefaultPageSize);

        await pagination.SetTotalItemCountAsync(iAccounts.TotalCount);
        foreach (var account in iAccounts.Data)
        {
            account.CreatedByUser = (await UserService.GetUserDetailsAsync(account.CreatedBy)).FullName;
        }

        accounts = iAccounts.Data.AsQueryable();

        //pagination.TotalItemCountChanged += (sender, EventArgs) => StateHasChanged();

    }

    private void HandleAccountFilter(ChangeEventArgs args)
    {
        if (args.Value is string value)
        {
            accountFilter = value;
        }
    }

    private void HandleClear()
    {
        if (string.IsNullOrWhiteSpace(accountFilter))
        {
            accountFilter = string.Empty;
        }
    }

    private async Task GoToPageAsync(int pageIndex)
    {
        await pagination.SetCurrentPageIndexAsync(pageIndex);
    }

    private Appearance PageButtonAppearance(int pageIndex)
        => pagination.CurrentPageIndex == pageIndex ? Appearance.Accent : Appearance.Neutral;

    private string? AriaCurrentValue(int pageIndex)
        => pagination.CurrentPageIndex == pageIndex ? "page" : null;

    private string AriaLabel(int pageIndex)
        => $"Go to page {pageIndex}";
}
