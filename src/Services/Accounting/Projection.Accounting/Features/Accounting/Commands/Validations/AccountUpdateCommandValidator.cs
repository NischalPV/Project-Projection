using FluentValidation;
using Projection.Accounting.Core.Entities;

namespace Projection.Accounting.Features.Accounting.Commands.Validations;

public class AccountUpdateCommandValidator: AbstractValidator<AccountUpdateCommand>
{
    public AccountUpdateCommandValidator(ILogger<AccountUpdateCommandValidator> logger)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Account name is required");

        RuleFor(x => x.PANNumber)
            .NotEmpty().WithMessage("PAN Number is required")
            .Length(10).WithMessage("PAN Number must be of 10 characters")
            .Matches(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$").WithMessage("PAN Number must be in Format XXXXX0000X");

        RuleFor(x => x.GSTNumber)
            .NotEmpty().WithMessage("GSTIN is required")
            .Length(15).WithMessage("GSTIN must be of 15 characters")
            .Matches(@"^\d{2}[A-Z]{5}\d{4}[A-Z]{1}[A-Z\d]{1}[Z]{1}[A-Z\d]{1}$").WithMessage("GSTIN must be in Format 00XXXXX0000X0Z0");

        RuleFor(x => x.Contacts)
            .Must(ContainAtLeastOneContact).WithMessage("At least one PoC is required")
            .ForEach(x => x.SetValidator(new PointOfContactValidator()));

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }

    private bool ContainAtLeastOneContact(IEnumerable<PointOfContact> contacts)
    {
        return contacts.Any();
    }
}
