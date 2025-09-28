using System.Data;
using FluentValidation;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Patients.Create;

public sealed record PatientCreateCommand(

    string FirstName,
    string Lastname,
    string IdentityNumber,
    string Country,
    string City,
    string Street,
    string Email,
    string PhoneNumber

) : IRequest<Result<string>>;

public sealed class PatientCreateCommandValidator : AbstractValidator<PatientCreateCommand>
{
    public PatientCreateCommandValidator()
    {

        RuleFor(rule => rule.FirstName)
            .MinimumLength(3)
            .WithMessage("First Name must be at least 3 characters.");

        RuleFor(rule => rule.Lastname)
            .MinimumLength(3)
            .WithMessage("Last Name must be at least 3 characters.");

        RuleFor(rule => rule.IdentityNumber)
            .MinimumLength(11)
            .WithMessage("Identity Number must be 11 characters.")
            .MaximumLength(11)
            .WithMessage("Identity Number must be 11 characters.");

    }
}
