using System.Security.Principal;
using eAppointment.Domain.Abstractions;
using eAppointment.Domain.Employees;
using eAppointment.Domain.Users;
using Microsoft.AspNetCore.Identity;
using TS.MediatR;

namespace eAppointment.Application.Employees;
public sealed record EmployeeGetAllQuery
(

)
: IRequest<IQueryable<EmployeeGetAllQueryResponse>>;

public sealed class EmployeeGetAllQueryResponse : EntityDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => string.Join(" ", FirstName, LastName);
    public DateOnly BirthDate { get; set; }
    public decimal Salary { get; set; }
    public string TcNo { get; set; } = default!;
    public string? Email { get; set; }
    public string? PhoneNumber1 { get; set; }
    public string? PhoneNumber2 { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Street { get; set; }
}

internal sealed class EmployeeGetAllQueryHandler(
    IEmployeeRepository employeeRepository,
    UserManager<AppUser> userManager
) : IRequestHandler<EmployeeGetAllQuery,
    IQueryable<EmployeeGetAllQueryResponse>>
{
    public Task<IQueryable<EmployeeGetAllQueryResponse>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
    {

        var response = (
                        from employee in employeeRepository.GetAll()
                        join create_user in userManager.Users.AsQueryable() on employee.CreatedUserId equals create_user.Id
                        // inner join because it might be null
                        join update_user in userManager.Users.AsQueryable() on employee.CreatedUserId equals update_user.Id into update_user
                            from update_users in update_user.DefaultIfEmpty()
                        select new EmployeeGetAllQueryResponse
                        {
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            Salary = employee.Salary,
                            BirthDate = employee.BirthDate,
                            CreatedAt = employee.CreatedAt,
                            DeletedAt = employee.DeletedAt,
                            Id = employee.Id,
                            IsDeleted = employee.IsDeleted,
                            TcNo = employee.PersonalInformation.TcNo,
                            Email = employee.PersonalInformation.Email,
                            PhoneNumber1 = employee.PersonalInformation.PhoneNumber1,
                            PhoneNumber2 = employee.PersonalInformation.PhoneNumber2,
                            City = employee.Address.City,
                            Country = employee.Address!.Country,
                            Street = employee.Address!.Street,
                            UpdatedAt = employee.UpdatedAt,
                            CreatedUserID = create_user.CreatedUserId,
                            CreatedUserName = create_user.FirstName + " " + create_user.LastName + " (" + create_user.Email + ")",
                            UpdatedUserID = employee.UpdatedUserId,
                            UpdatedUserName = employee.UpdatedUserId == null ? null : update_users.FirstName + " " + update_users.LastName + " (" + update_users.Email + ")",
                        })
                        .AsQueryable();
        
        return Task.FromResult(response);

    }
}