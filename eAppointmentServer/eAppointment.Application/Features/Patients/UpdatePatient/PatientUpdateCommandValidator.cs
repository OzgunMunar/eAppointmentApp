using FluentValidation;

namespace eAppointment.Application.Features.Patients.PatientUpdate;

public sealed class PatientUpdateCommandValidator : AbstractValidator<PatientUpdateCommand>
{
    public PatientUpdateCommandValidator()
    {
        RuleFor(ruleFor => ruleFor.FirstName)
            .MinimumLength(3)
            .WithMessage("FirstName must be at least 3 characters.");

        RuleFor(ruleFor => ruleFor.LastName)
            .MinimumLength(3)
            .WithMessage("LastName must be at least 3 characters.");

        RuleFor(ruleFor => ruleFor.IdentityNumber)
            .MinimumLength(11)
                .WithMessage("Length must be 11 characters.")
            .MaximumLength(11)
                .WithMessage("Length must be 11 characters.");

    }
}
