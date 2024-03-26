using MediatR;
using Projection.BuildingBlocks.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.Accounting.Commands;

public class IdentifiedCommand<TCommand, TResponse>: IRequest<TResponse> where TCommand: IRequest<TResponse>
{
    public TCommand Command { get; }
    public Guid Id { get; }
    public IdentifiedCommand(TCommand command, Guid id)
    {
        Command = command;
        Id = id;
    }
}
