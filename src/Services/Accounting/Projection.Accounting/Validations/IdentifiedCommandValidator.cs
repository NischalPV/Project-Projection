using FluentValidation;
using Microsoft.Extensions.Logging;
using Projection.Accounting.Commands;
using Projection.BuildingBlocks.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.Accounting.Validations;

public class IdentifiedCommandValidator<T, R> : AbstractValidator<IdentifiedCommand<T, R>> where T : BaseCommand<R>
{
    public IdentifiedCommandValidator(ILogger<IdentifiedCommandValidator<T, R>> logger)
    {
        RuleFor(command => command.Id).NotEmpty();

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
