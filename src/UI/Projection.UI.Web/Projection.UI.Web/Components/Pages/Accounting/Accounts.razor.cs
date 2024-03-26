using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Projection.UI.Web.Models;

namespace Projection.UI.Web.Components.Pages.Accounting;

public partial class Accounts
{
    private IQueryable<Account>? accounts;
    PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
    string accountFilter = string.Empty;

    IQueryable<Account>? FilteredItems => accounts?.Where(x => x.AccountNumber.Contains(accountFilter, StringComparison.CurrentCultureIgnoreCase));

    protected override async Task OnInitializedAsync()
    {
        var iAccounts = await AccountingService.GetAccounts();

        foreach (var account in iAccounts.Data)
        {
            account.CreatedByUser = (await UserService.GetUserDetailsAsync(account.CreatedBy)).FullName;
        }

        //iAccounts.Data.ForEach(async account => account.CreatedByUser = (await UserService.GetUserDetailsAsync(account.CreatedBy)).FullName);

        accounts = iAccounts.Data.AsQueryable();
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
}
