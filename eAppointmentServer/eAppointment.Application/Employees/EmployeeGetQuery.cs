using eAppointment.Domain.Employees;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Employees;
public sealed record EmployeeGetQuery(
    Guid UserId
) : IRequest<Result<Employee>>;

internal sealed class EmployeeGetQueryHandler(
    IEmployeeRepository employeeRepository
) : IRequestHandler<EmployeeGetQuery, Result<Employee>>
{
    public async Task<Result<Employee>> Handle(EmployeeGetQuery request, CancellationToken cancellationToken)
    {

        var employee = await employeeRepository.FirstOrDefaultAsync(employee => employee.Id == request.UserId, cancellationToken);

        if (employee is null)
        {
            return Result<Employee>.Failure("Employee is not found.");
        }

        return employee;

    }
}