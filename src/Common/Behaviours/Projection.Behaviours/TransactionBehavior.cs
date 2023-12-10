namespace Projection.Common.Behaviours;

public class TransactionBehavior<TRequest, TResponse, TContext> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TContext : BaseDbContext
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse, TContext>> _logger;
    private readonly TContext _dbContext;
    private readonly IApiIntegrationEventService<BaseDbContext> _integrationEventService;

    public TransactionBehavior(TContext dbContext,
        IApiIntegrationEventService<BaseDbContext> integrationEventService,
        ILogger<TransactionBehavior<TRequest, TResponse, TContext>> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentException(nameof(TContext));
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

                await _integrationEventService.PublishEventsThroughEventBusAsync(transactionId, typeof(TContext).Name);
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