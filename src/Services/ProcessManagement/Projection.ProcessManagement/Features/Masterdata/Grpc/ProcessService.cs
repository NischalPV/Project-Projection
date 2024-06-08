using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Projection.BuildingBlocks.Shared.Models.ViewModels;
using Projection.ProcessManagement.Extensions;
using Projection.ProcessManagement.Features.Masterdata.Commands;
using Projection.ProcessManagement.Grpc;
using Projection.ProcessManagement.Infrastructure.Data;

namespace Projection.ProcessManagement.Features.Masterdata.Grpc;

public class ProcessService(IBaseEntityAsyncRepository<ProcessManagement.Core.Entities.Process, Guid, ProcessManagementDbContext> repository,
                            ILogger<ProcessService> logger,
                            IMediator mediator) : Processes.ProcessesBase
{
    public override async Task<ResultViewModel> CreateAsync(CreateRequest request, ServerCallContext context)
    {
        Guid requestId = Guid.NewGuid();
        var processCreateCommand = new ProcessCreateCommand()
        {
            Name = request.Process.Name,
            Description = request.Process.Description,
            IsMandatory = request.Process.IsMandatory,
            IsAutomated = request.Process.IsAutomated,
            CreatedBy = context.GetUserIdentity()
        };

        foreach (var item in request.Process.AdditionalAttributes)
        {
            processCreateCommand.AdditionalAttributes.Add(item.Key, item.Value);
        }

        logger.LogInformation("Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})", nameof(ProcessCreateCommand), nameof(requestId), requestId, processCreateCommand);

        var result = await mediator.Send(processCreateCommand);

        if(result is null)
        {
            logger.LogWarning("ProcessCreateCommand failed - RequestId: {RequestId}", requestId);
            return new ResultViewModel()
            {
                IsSuccess = false,

                Result = new ProcessManagement.Grpc.ResultMessage()
                {
                    Message = "ProcessCreateCommand failed",
                    Code = StatusCodes.Status500InternalServerError,
                    Description = "ProcessCreateCommand failed",
                    Type = "Error",
                    Field = "ProcessCreateCommand"
                },

                PageCount = 0,
                PageNumber = 0,
                PageSize = 0,
                TotalCount = 0
            };
        }

        logger.LogInformation("ProcessCreateCommand succeeded - RequestId: {RequestId}", requestId);

        return new ResultViewModel()
        {
            Data = Any.Pack((Google.Protobuf.IMessage)result),
            Result = new ProcessManagement.Grpc.ResultMessage()
            {
                Message = "Success",
                Code = StatusCodes.Status201Created,
                Description = "Success",
                Type = "Success",
                Field = ""
            },
            IsSuccess = true,
            PageCount = 1,
            PageNumber = 1,
            PageSize = 1,
            TotalCount = 1
        };
    }
}
