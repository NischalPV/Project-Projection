using MediatR;
using Projection.BuildingBlocks.IntegrationEventLogEF;
using Projection.Common.IntegrationService;
using Projection.ProcessManagement.Core.Entities;
using Projection.ProcessManagement.Features.Masterdata.IntegrationEvents;
using Projection.ProcessManagement.Infrastructure.Data;
using Projection.ServiceDefaults.Services;

namespace Projection.ProcessManagement.Features.Masterdata.Commands;

public class ProcessCreateCommandHandler(IBaseEntityAsyncRepository<Process, Guid, ProcessManagementDbContext> repository,
                                         ILogger<ProcessCreateCommandHandler> logger,
                                         IIdentityService identityService,
                                         IApiIntegrationEventService<IntegrationEventLogContext> apiIntegrationEventService) : IRequestHandler<ProcessCreateCommand, Process>
{

    public async Task<Process> Handle(ProcessCreateCommand request, CancellationToken cancellationToken)
    {
        var loggedInUserId = identityService.GetUserIdentity();

        var processCreatedEvent = new ProcessCreatedIntegrationEvent()
        {
            Name = request.Name,
            Description = request.Description,
            CreatedBy = loggedInUserId,
            IsMandatory = request.IsMandatory,
            IsAutomated = request.IsAutomated,
            AdditionalAttributes = request.AdditionalAttributes,
            StatusId = 1
        };

        await apiIntegrationEventService.AddAndSaveEventAsync(processCreatedEvent);

        var process = new Process()
        {
            Name = request.Name,
            Description = request.Description,
            CreatedBy = loggedInUserId,
            IsMandatory = request.IsMandatory,
            IsAutomated = request.IsAutomated,
            AdditionalAttributes = request.AdditionalAttributes
        };

        logger.LogInformation("-------Creating Process - Process: {@Process}", process);

        var newProcess = await repository.AddAsync(process, false);

        await repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return newProcess;
    }
}
