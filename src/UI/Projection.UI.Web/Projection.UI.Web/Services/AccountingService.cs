using Projection.BuildingBlocks.Shared.Models.ViewModels;
using Projection.UI.Web.Models;
using System.Collections.Generic;

namespace Projection.UI.Web.Services;

public class AccountingService(HttpClient httpClient)
{
    private readonly string remoteServiceBaseUrl = "/accounting/api/accounts";

    public Task<ResultViewModel<List<Account>>> GetAccounts(int pageIndex, int pageSize)
    {
        httpClient.DefaultRequestHeaders.Add("pageIndex", $"{pageIndex}");
        httpClient.DefaultRequestHeaders.Add("pageSize", $"{pageSize}");
        var accountData = httpClient.GetFromJsonAsync<ResultViewModel<List<Account>>>($"{remoteServiceBaseUrl}")!;

        return accountData;
    }
}