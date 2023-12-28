using MediatR;
using Microsoft.Extensions.Logging;
using Projection.Accounting.Features.Accounting.Commands;
using Projection.BuildingBlocks.EventBus.Extensions;
using Projection.BuildingBlocks.Shared.Commands;
using Projection.BuildingBlocks.Shared.Idempotency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.Accounting.Commands;

/// <summary>
/// Provides a base implementation for handling duplicate request and ensuring idempotent updates, in the cases where
/// a requestid sent by client is used to detect duplicate requests.
/// </summary>
/// <typeparam name="TCommand">Type of the command handler that performs the operation if request is not duplicated</typeparam>
/// <typeparam name="TResponse">Return value of the inner command handler</typeparam>
public abstract class IdentifiedCommandHandler<TCommand, TResponse> : IRequestHandler<IdentifiedCommand<TCommand, TResponse>, TResponse>
    where TCommand : IRequest<TResponse>
{
    private readonly IMediator _mediator;
    private readonly IRequestManager _requestManager;
    private readonly ILogger<IdentifiedCommandHandler<TCommand, TResponse>> _logger;

    protected IdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager, ILogger<IdentifiedCommandHandler<TCommand, TResponse>> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _requestManager = requestManager ?? throw new ArgumentNullException(nameof(requestManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Creates the result value to return if a previous request was found
    /// </summary>
    /// <returns></returns>
    protected abstract TResponse CreateResultForDuplicateRequest();

    public async Task<TResponse> Handle(IdentifiedCommand<TCommand, TResponse> request, CancellationToken cancellationToken)
    {
        var alreadyExists = await _requestManager.ExistAsync(request.Id);
        if (alreadyExists)
        {
            return CreateResultForDuplicateRequest();
        }
        else
        {
            await _requestManager.CreateRequestForCommandAsync<TCommand>(request.Id);

            try
            {
                var command = request.Command;
                var commandName = command.GetGenericTypeName();
                var idProperty = String.Empty;
                var commandId = String.Empty;

                switch (command)
                {
                    case AccountCreateCommand accountCreateCommand:
                        idProperty = nameof(accountCreateCommand.AccountNumber);
                        commandId = accountCreateCommand.AccountNumber;
                        break;

                    default:
                        idProperty = "Id?";
                        commandId = "n/a";
                        break;
                }

                _logger.LogInformation("----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                                       commandName,
                                       idProperty,
                                       commandId,
                                       command);

                TResponse result = (TResponse)await _mediator.Send(request.Command, cancellationToken);

                _logger.LogInformation("Command result: {@Result} - {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                                       result,
                                       commandName,
                                       idProperty,
                                       commandId,
                                       command);

                return result;

            }
            catch (Exception ex)
            {

                //_logger.LogError(
                //    ex,
                //    "ERROR Handling command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                //    request.Command.GetGenericTypeName(),
                //    nameof(request.Command.Id),
                //    request.Command.Id,
                //    request.Command);

                return default;
            }
        }
    }
}
