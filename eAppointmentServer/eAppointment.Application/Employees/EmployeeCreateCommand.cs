using eAppointment.Domain.Employees;
using FluentValidation;
using GenericRepository;
using Mapster;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Employees;

public sealed record EmployeeCreateCommand
(
    string FirstName,
    string LastName,
    DateOnly BirthDate,
    decimal Salary,
    PersonalInformation? PersonalInformation,
    Address? Address,
    bool IsActive
) : IRequest<Result<string>>;

public sealed class EmployeeCreateCommandValidator : AbstractValidator<EmployeeCreateCommand>
{
    public EmployeeCreateCommandValidator()
    {
        RuleFor(ruleFor => ruleFor.FirstName)
            .MinimumLength(3)
            .WithMessage("FirstName must be at least 3 characters");

        RuleFor(ruleFor => ruleFor.LastName)
            .MinimumLength(3)
            .WithMessage("LastName must be at least 3 characters.");

        RuleFor(ruleFor => ruleFor.PersonalInformation!.TcNo)
            .MinimumLength(11)
                .WithMessage("Length must be 11 characters;")
            .MaximumLength(11)
                .WithMessage("Length must be 11 characters;");
    }
}

internal sealed class EmployeeCreateCommandHandler(
    IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork
) : IRequestHandler<EmployeeCreateCommand, Result<string>>
{
    async Task<Result<string>> IRequestHandler<EmployeeCreateCommand, Result<string>>.Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
    {

        bool searchEmployee = await employeeRepository.AnyAsync(emp => emp.PersonalInformation.TcNo == request.PersonalInformation!.TcNo, cancellationToken);

        if (searchEmployee)
            return Result<string>.Failure("Personnal with same TcNo already exist.");

        Employee employee = request.Adapt<Employee>();
        employeeRepository.Add(employee);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Personnal is saved successfully.");

    }
}