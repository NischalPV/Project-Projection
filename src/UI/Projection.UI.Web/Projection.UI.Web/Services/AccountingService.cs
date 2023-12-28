using Projection.BuildingBlocks.Shared.Models.ViewModels;
using Projection.UI.Web.Models;
using System.Collections.Generic;

namespace Projection.UI.Web.Services;

public class AccountingService
{
    private readonly string remoteServiceBaseUrl = "/api/accounts";

    private readonly ILogger<AccountingService> _logger;

    private readonly UserService _userService;

    private readonly HttpClient _httpClient;

    public AccountingService(HttpClient httpClient, ILogger<AccountingService> logger, UserService userService)
    {
        this._httpClient = httpClient;
        this._logger = logger;
        this._userService = userService;
    }

    public Task<ResultViewModel<List<Account>>> GetAccounts()
    {
        var accountData = _httpClient.GetFromJsonAsync<ResultViewModel<List<Account>>>(remoteServiceBaseUrl)!;



        return accountData;
    }
}