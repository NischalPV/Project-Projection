using FluentValidation;
using Projection.Accounting.Core.Entities;

namespace Projection.Accounting.Features.Accounting.Commands.Validations;

public class AccountCreateCommandValidator : AbstractValidator<AccountCreateCommand>
{
    public AccountCreateCommandValidator(ILogger<AccountCreateCommandValidator> logger)
    {

        RuleFor(x => x.Name).NotEmpty().WithMessage("Account name is required");

        RuleFor(x => x.CurrencyId).NotEmpty().WithMessage("Currency is required");

        RuleFor(x => x.Balance).NotEmpty().WithMessage("Balance is required");

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

public class PointOfContactValidator : AbstractValidator<PointOfContact>
{
    public PointOfContactValidator()
    {
        
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
            .EmailAddress();

        RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone Number is required")
            //Phone number validation with country code
            .Matches(@"^[+]?[0-9]{10,13}$").WithMessage("Phone Number must be of 10-13 digits");
    }
}

