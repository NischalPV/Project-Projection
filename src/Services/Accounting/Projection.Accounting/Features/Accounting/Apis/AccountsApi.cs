using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Projection.Accounting.Commands;
using Projection.Accounting.Core.Entities;
using Projection.Accounting.Features.Accounting.Commands;
using Projection.Accounting.Features.Accounting.Requests;
using Projection.Accounting.Features.Accounting.Services;
using Projection.BuildingBlocks.EventBus.Extensions;

namespace Projection.Accounting.Features.Accounting.Apis;

public static class AccountsApi
{
    public static RouteGroupBuilder MapAccountsApi(this RouteGroupBuilder app)
    {
        app.MapGet("/", GetAccounts);
        app.MapGet("/{id}", GetAccount);
        app.MapPost("/", CreateAccount);
            
        return app;
    }

    /// <summary>
    /// Gets all accounts
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static async Task<Ok<List<Account>>> GetAccounts([AsParameters] AccountServices services)
    {
        var accounts = await services.Repository.ListAllAsync();
        return TypedResults.Ok(accounts);
    }

    /// <summary>
    /// Gets an account by id
    /// </summary>
    /// <param name="id">Account Id</param>
    /// <param name="services"></param>
    /// <returns></returns>
    public static async Task<Results<Ok<Account>, BadRequest<string>>> GetAccount(string id, [AsParameters] AccountServices services)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return TypedResults.BadRequest("Id is required");
        }
        var account = await services.Repository.GetByIdAsync(id);
        return TypedResults.Ok(account);
    }


    public static async Task<Results<Created<Account>, BadRequest<string>, ProblemHttpResult>> CreateAccount([FromHeader(Name = "x-requestid")] Guid requestId, CreateAccountRequest request, [AsParameters] AccountServices services)
    {
        services.Logger.LogInformation(
             "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
             request.GetGenericTypeName(),
             nameof(request.Id),
             request.Id,
             request);

        if (requestId == Guid.Empty)
        {
            services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
            return TypedResults.BadRequest("RequestId is missing.");
        }

        using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
        {
            var loggedInUser = services.IdentityService.GetUserIdentity();
            var command = new AccountCreateCommand(request.Name, request.AccountNumber, request.GSTNumber, request.PANNumber, request.CurrencyId, request.Balance, loggedInUser, request.Description);

            var requestCreateAccount = new IdentifiedCommand<AccountCreateCommand, Account>(command, requestId);

            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                requestCreateAccount.GetGenericTypeName(),
                nameof(requestCreateAccount.Id),
                requestCreateAccount.Id,
                requestCreateAccount);

            var result = await services.Mediator.Send(command);

            if (result is not null)
            {
                services.Logger.LogInformation("AccountCreateCommand succeeded - RequestId: {RequestId}", requestId);
                return TypedResults.Created<Account>(String.Empty, result);
            }
            else
            {
                services.Logger.LogWarning("AccountCreateCommand failed - RequestId: {RequestId}", requestId);
                return TypedResults.Problem(detail: "AccountCreateCommand failed", statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }

}
