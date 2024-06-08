using MediatR;
using Microsoft.EntityFrameworkCore;
using Projection.BuildingBlocks.EventBus.Extensions;
using Projection.BuildingBlocks.IntegrationEventLogEF;
using Projection.BuildingBlocks.Shared.Commands;
using Projection.Common.IntegrationService;
using Projection.ProcessManagement.Infrastructure.Data;

namespace Projection.ProcessManagement.Behaviours;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : BaseCommand<TResponse>
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
    private readonly ProcessManagementDbContext _dbContext;
    private readonly IApiIntegrationEventService<IntegrationEventLogContext> _integrationEventService;

    public TransactionBehavior(ProcessManagementDbContext dbContext,
        IApiIntegrationEventService<IntegrationEventLogContext> integrationEventService,
        ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        _integrationEventService = integrationEventService ?? throw new ArgumentException(nameof(integrationEventService));
        _logger = logger ?? throw new ArgumentException(nameof(ILogger));
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = request.GetGenericTypeName();

        try
        {
            if (_dbContext.HasActiveTransaction)
            {
                return await next();
            }

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                Guid transactionId;

                await using var transaction = await _dbContext.BeginTransactionAsync();
                using (_logger.BeginScope(new List<KeyValuePair<string, object>>
                {
                    new("TransactionContext", transaction.TransactionId)
                }))
                {
                    _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                    response = await next();

                    _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                    await _dbContext.CommitTransactionAsync(transaction);

                    transactionId = transaction.TransactionId;
                }

                await _integrationEventService.PublishEventsThroughEventBusAsync(transactionId, typeof(ProcessManagementDbContext).Name);
            });

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }
    }
}
