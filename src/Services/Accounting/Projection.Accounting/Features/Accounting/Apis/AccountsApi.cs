using Asp.Versioning.Conventions;
using CsvHelper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Projection.Accounting.Commands;
using Projection.Accounting.Core.Entities;
using Projection.Accounting.Features.Accounting.Commands;
using Projection.Accounting.Features.Accounting.Requests;
using Projection.Accounting.Features.Accounting.Services;
using Projection.Accounting.Features.Accounting.Specifications;
using Projection.BuildingBlocks.EventBus.Extensions;
using Projection.BuildingBlocks.Shared.Models.ViewModels;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using static Projection.Accounting.Features.Accounting.Commands.AccountUploadCommandHandler;

namespace Projection.Accounting.Features.Accounting.Apis;

public static class AccountsApi
{
    public static RouteGroupBuilder MapAccountsApi(this RouteGroupBuilder app)
    {
        app.MapGet("/{pageIndex:int}", GetAccountsAsync);
        app.MapGet("/{id:guid}", GetAccountAsync);
        app.MapPost("/", CreateAccountAsync);
        app.MapPut("/{id}", UpdateAccountAsync);
        app.MapDelete("/{id}", DeleteAccountAsync);
        app.MapPost("/upload", Upload);

        return app;
    }

    /// <summary>
    /// Gets all accounts
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static async Task<Ok<ResultViewModel<List<Account>>>> GetAccountsAsync(int pageIndex, [AsParameters] AccountServices services)
    {
        var accounts = await services.Repository.ListAllAsync(new AccountWithStatusSpecification(pageIndex));

        ResultViewModel<List<Account>> result = new()
        {
            Data = accounts ?? new List<Account>(),
            IsSuccess = true,
            Result = new ResultMessage()
            {
                Code = StatusCodes.Status200OK,
                Description = "Success",
                Field = "",
                Message = "Success",
                Type = "Success"
            },
            PageCount = 1,
            PageNumber = 1,
            PageSize = 25,
            TotalCount = accounts.Count,
        };

        return TypedResults.Ok(result);
    }

    /// <summary>
    /// Gets an account by id
    /// </summary>
    /// <param name="id">Account Id</param>
    /// <param name="services"></param>
    /// <returns></returns>
    public static async Task<Results<Ok<ResultViewModel<Account>>, BadRequest<string>>> GetAccountAsync(string id, [AsParameters] AccountServices services)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return TypedResults.BadRequest("Id is required");
        }
        var account = await services.Repository.GetByIdAsync(id);

        ResultViewModel<Account> result = new();

        result.Data = account;

        if (account is null)
        {
            result.IsSuccess = false;
            result.Result = new ResultMessage()
            {
                Code = StatusCodes.Status404NotFound,
                Description = "Account not found",
                Field = "",
                Message = "Account not found",
                Type = "Error"
            };
            result.PageCount = 0;
            result.PageNumber = 0;
            result.PageSize = 0;
            result.TotalCount = 0;

        }
        else
        {
            result.IsSuccess = true;
            result.Result = new ResultMessage()
            {
                Code = StatusCodes.Status200OK,
                Description = "Success",
                Field = "",
                Message = "Success",
                Type = "Success"
            };
            result.PageCount = 1;
            result.PageNumber = 1;
            result.PageSize = 1;
            result.TotalCount = 1;

        }


        return TypedResults.Ok(result);
    }


    /// <summary>
    /// Create a new account
    /// </summary>
    /// <param name="requestId">unique uuid for each create request</param>
    /// <param name="request">Create Account request</param>
    /// <param name="services"></param>
    /// <returns>Created Account details, if successful</returns>
    public static async Task<Results<Created<ResultViewModel<Account>>, BadRequest<string>, JsonHttpResult<ResultViewModel<Account>>>> CreateAccountAsync([FromHeader(Name = "x-requestid")] Guid requestId, CreateAccountRequest request, [AsParameters] AccountServices services)
    {
        services.Logger.LogInformation(
             "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
             request.GetGenericTypeName(),
             nameof(request.GSTNumber),
             request.GSTNumber,
             request);

        if (requestId == Guid.Empty)
        {
            services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
            return TypedResults.BadRequest("RequestId is missing.");
        }

        using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
        {
            var loggedInUser = services.IdentityService.GetUserIdentity();
            var command = new AccountCreateCommand(request.Name, request.GSTNumber.ToUpper(), request.PANNumber.ToUpper(), request.Currency, request.Balance, loggedInUser, request.Description, request.Contacts);

            var requestCreateAccount = new IdentifiedCommand<AccountCreateCommand, Account>(command, requestId);

            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                requestCreateAccount.GetGenericTypeName(),
                nameof(requestCreateAccount.Id),
                requestCreateAccount.Id,
                requestCreateAccount);

            var result = await services.Mediator.Send(command);

            ResultViewModel<Account> response = new();
            response.Data = result;

            if (result is not null)
            {
                services.Logger.LogInformation("AccountCreateCommand succeeded - RequestId: {RequestId}", requestId);

                response.IsSuccess = true;
                response.Result = new ResultMessage()
                {
                    Code = StatusCodes.Status200OK,
                    Description = "Success",
                    Field = "",
                    Message = "Success",
                    Type = "Success"
                };
                response.PageCount = 1;
                response.PageNumber = 1;
                response.PageSize = 1;
                response.TotalCount = 1;

                return TypedResults.Created<ResultViewModel<Account>>(String.Empty, response);
            }
            else
            {
                services.Logger.LogWarning("AccountCreateCommand failed - RequestId: {RequestId}", requestId);

                response.IsSuccess = false;
                response.Result = new ResultMessage()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Description = "AccountCreateCommand failed",
                    Field = "",
                    Message = "AccountCreateCommand failed",
                    Type = "Error"
                };
                response.PageCount = 0;
                response.PageNumber = 0;
                response.PageSize = 0;
                response.TotalCount = 0;


                return TypedResults.Json<ResultViewModel<Account>>(response, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }


    /// <summary>
    /// Updated existing account
    /// </summary>
    /// <param name="requestId">Unique uuid for each request</param>
    /// <param name="id">Account Id to be updated</param>
    /// <param name="request">Account Update request</param>
    /// <param name="services"></param>
    /// <returns>Updated Account details</returns>
    public static async Task<Results<Ok<Account>, BadRequest<string>, ProblemHttpResult>> UpdateAccountAsync([FromHeader(Name = "x-requestid")] Guid requestId, string id, UpdateAccountRequest request, [AsParameters] AccountServices services)
    {
        services.Logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            request.GetGenericTypeName(),
            nameof(request.GSTNumber),
            request.GSTNumber,
            request);

        if (requestId == Guid.Empty)
        {
            services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
            return TypedResults.BadRequest("RequestId is missing.");
        }

        if (string.IsNullOrWhiteSpace(id))
        {
            services.Logger.LogWarning("Invalid IntegrationEvent - Id is missing - {@IntegrationEvent}", request);
            return TypedResults.BadRequest("Id is missing.");
        }

        if (id != request.Id)
        {
            services.Logger.LogWarning("Invalid IntegrationEvent - Id is not matching - {@IntegrationEvent}", request);
            return TypedResults.BadRequest("UpdateAccountRequest Id must match the Id parameter");
        }

        using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
        {
            var loggedInUser = services.IdentityService.GetUserIdentity();
            var command = new AccountUpdateCommand(request.Id, request.Name, request.GSTNumber.ToUpper(), request.PANNumber.ToUpper(), loggedInUser, request.Description, request.Contacts);

            var requestUpdateAccount = new IdentifiedCommand<AccountUpdateCommand, Account>(command, requestId);

            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                requestUpdateAccount.GetGenericTypeName(),
                nameof(requestUpdateAccount.Id),
                requestUpdateAccount.Id,
                requestUpdateAccount);

            var result = await services.Mediator.Send(command);

            if (result is not null)
            {
                services.Logger.LogInformation("AccountUpdateCommand succeeded - RequestId: {RequestId}", requestId);
                return TypedResults.Ok(result);
            }
            else
            {
                services.Logger.LogWarning("AccountUpdateCommand failed - RequestId: {RequestId}", requestId);
                return TypedResults.Problem(detail: "AccountUpdateCommand failed", statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }

    /// <summary>
    /// Delete existing account
    /// </summary>
    /// <param name="requestId">
    /// Unique uuid for each request
    /// </param>
    /// <param name="id">
    /// Account Id to be deleted
    /// </param>
    /// <param name="services"></param>
    /// <returns>
    /// True if account is deleted successfully
    /// </returns>
    public static async Task<Results<Ok<bool>, BadRequest<string>, ProblemHttpResult>> DeleteAccountAsync([FromHeader(Name = "x-requestid")] Guid requestId, string id, [AsParameters] AccountServices services)
    {
        services.Logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId})",
            nameof(AccountTerminateCommand),
            nameof(AccountTerminateCommand.Id),
            id);

        if (requestId == Guid.Empty)
        {
            services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", id);
            return TypedResults.BadRequest("RequestId is missing.");
        }

        if (string.IsNullOrWhiteSpace(id))
        {
            services.Logger.LogWarning("Invalid IntegrationEvent - Id is missing - {@IntegrationEvent}", id);
            return TypedResults.BadRequest("Id is missing.");
        }

        using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
        {
            var command = new AccountTerminateCommand(id);

            var requestDeleteAccount = new IdentifiedCommand<AccountTerminateCommand, string>(command, requestId);

            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                requestDeleteAccount.GetGenericTypeName(),
                nameof(requestDeleteAccount.Id),
                requestDeleteAccount.Id,
                requestDeleteAccount);

            var result = await services.Mediator.Send(command);

            if (result == String.Empty)
            {
                services.Logger.LogInformation("AccountDeleteCommand succeeded - RequestId: {RequestId}", requestId);
                return TypedResults.Ok(true);
            }
            else
            {
                services.Logger.LogWarning("AccountDeleteCommand failed - RequestId: {RequestId}", requestId);
                return TypedResults.Problem(detail: $"AccountDeleteCommand failed. Reason: {result}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }

    public static async Task<Results<Created<ResultViewModel<bool>>, BadRequest<string>, JsonHttpResult<ResultViewModel<RangeResult>>>> Upload([FromHeader(Name = "x-requestid")] Guid requestId, UploadAccountsFileRequest request, [AsParameters] AccountServices services)
    {
        services.Logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId})",
            nameof(AccountUploadCommand),
            nameof(AccountUploadCommand.Id),
            request.AccountsFile);

        if (requestId == Guid.Empty)
        {
            services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request.AccountsFile);
            return TypedResults.BadRequest("RequestId is missing.");
        }

        if (request.AccountsFile is null)
        {
            services.Logger.LogWarning("Invalid IntegrationEvent - File is missing - {@IntegrationEvent}", request.AccountsFile);
            return TypedResults.BadRequest("File is missing.");
        }

        using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
        {
            var command = new AccountUploadCommand(request.AccountsFile);

            //var requestUploadAccount = new IdentifiedCommand<AccountUploadCommand, string>(command, requestId);

            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                command.GetGenericTypeName(),
                nameof(command.Id),
                command.Id,
                command);

            var result = await services.Mediator.Send(command);

            ResultViewModel<bool> response = new();
            response.Data = result;
            response.IsSuccess = true;
            response.Result = new ResultMessage()
            {
                Code = StatusCodes.Status200OK,
                Description = "Success",
                Field = "",
                Message = result == true ? $"File uploaded successfully." : "File upload failed.",
                Type = "Success"
            };
            

            return TypedResults.Created(String.Empty, response);
        }
    }
}
