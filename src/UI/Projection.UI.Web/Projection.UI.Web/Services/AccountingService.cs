using Projection.BuildingBlocks.Shared.Models.ViewModels;
using Projection.UI.Web.Models;
using System.Collections.Generic;

namespace Projection.UI.Web.Services;

public class AccountingService(HttpClient httpClient)
{
    private readonly string remoteServiceBaseUrl = "/accounting/api/accounts";

    public Task<ResultViewModel<List<Account>>> GetAccounts()
    {
        var accountData = httpClient.GetFromJsonAsync<ResultViewModel<List<Account>>>(remoteServiceBaseUrl)!;

        return accountData;
    }
}