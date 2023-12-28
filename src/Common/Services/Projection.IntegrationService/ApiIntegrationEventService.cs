using System;
using System.Data.Common;
using Projection.Common.DataService.Contexts;
using Projection.BuildingBlocks.EventBus.Abstractions;
using Projection.BuildingBlocks.EventBus.Events;
using Projection.BuildingBlocks.IntegrationEventLogEF.Services;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Projection.Common.IntegrationService;

public class ApiIntegrationEventService<TContext> : IApiIntegrationEventService<TContext> where TContext : BaseDbContext
{
    private readonly IEventBus _eventBus;
    private readonly TContext _dbContext;
    private readonly IIntegrationEventLogService _eventLogService;
    private readonly ILogger<ApiIntegrationEventService<TContext>> _logger;

    public ApiIntegrationEventService(IEventBus eventBus, TContext dbContext, IIntegrationEventLogService eventLogService, ILogger<ApiIntegrationEventService<TContext>> logger)
    {
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _eventLogService = eventLogService ?? throw new ArgumentNullException(nameof(eventLogService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task AddAndSaveEventAsync(IntegrationEvent evt)
    {
        _logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

        await _eventLogService.SaveEventAsync(evt, _dbContext.GetCurrentTransaction());
    }

    public async Task PublishEventsThroughEventBusAsync(Guid transactionId, string appName)
    {
        var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

        foreach (var logEvt in pendingLogEvents)
        {
            _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", logEvt.EventId, appName, logEvt.IntegrationEvent);

            try
            {
                await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                await _eventBus.PublishAsync(logEvt.IntegrationEvent);
                await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", logEvt.EventId, appName);

                await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
            }
        }
    }
}