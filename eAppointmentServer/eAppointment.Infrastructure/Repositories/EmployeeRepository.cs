using eAppointment.Domain.Employees;
using eAppointment.Infrastructure.Context;
using GenericRepository;

namespace eAppointment.Infrastructure.Repositories;

public sealed class EmployeeRepository(ApplicationDbContext context) : Repository<Employee, ApplicationDbContext>(context), IEmployeeRepository
{
}