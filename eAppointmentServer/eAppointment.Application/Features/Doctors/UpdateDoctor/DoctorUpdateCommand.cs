using FluentValidation;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Doctors.UpdateDoctor;    

public sealed record DoctorUpdateCommand
(
    Guid Id,
    string FirstName,
    string LastName,
    string IdentityNumber,
    string Country,
    string City,
    string Street,
    string Email,
    string PhoneNumber,
    int Department

) : IRequest<Result<string>>;

public sealed class DoctorUpdateCommandValidator : AbstractValidator<DoctorUpdateCommand>
{
    public DoctorUpdateCommandValidator()
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
