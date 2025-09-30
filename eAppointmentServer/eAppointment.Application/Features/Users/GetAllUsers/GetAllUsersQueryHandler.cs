using eAppointment.Domain.Repositories;
using eAppointment.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;

namespace eAppointmentServer.Application.Features.Users.GetAllUsers;

internal sealed class GetAllUsersQueryHandler(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    IUserRoleRepository userRoleRepository
) :
IRequestHandler<GetAllUsersQuery, IQueryable<GetAllUsersQueryResponse>>
{
    public async Task<IQueryable<GetAllUsersQueryResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {

        var userList = await userManager.Users.ToListAsync(cancellationToken);
        var userRolesList = await userRoleRepository.GetAllAppUserRolesAsync(cancellationToken);
        var rolesList = await roleManager.Roles.ToListAsync(cancellationToken);

        var users = (

            from user in userList

            join userRole in userRolesList on user.Id equals userRole.UserId
            into userRolesGroup
            from userRole in userRolesGroup.DefaultIfEmpty()

            join role in rolesList on userRole.RoleId equals role.Id
            into rolesGroup
            from role in rolesGroup.DefaultIfEmpty()

            where user.IsActive == true

            select new GetAllUsersQueryResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName!,
                Email = user.Email!,
                RoleId = role.Id,
                RoleName = role.Name!,
                IsActive = user.IsActive
            }

        ).ToList();

        return users.AsQueryable();

    }
}