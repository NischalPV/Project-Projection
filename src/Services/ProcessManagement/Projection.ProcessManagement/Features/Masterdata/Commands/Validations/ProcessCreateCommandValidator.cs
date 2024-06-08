using FluentValidation;

namespace Projection.ProcessManagement.Features.Masterdata.Commands.Validations;

public class ProcessCreateCommandValidator : AbstractValidator<ProcessCreateCommand>
{
    public ProcessCreateCommandValidator(ILogger<ProcessCreateCommandValidator> logger)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
