using eAppointment.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;

namespace eAppointment.Application.Features.Roles.GetAllRoles;

public sealed record GetAllRolesQuery():
IRequest<IQueryable<AppRole>>;


internal sealed class GetAllRolesQueryHandler(
    RoleManager<AppRole> roleManager
) : IRequestHandler<GetAllRolesQuery, IQueryable<AppRole>>
{
    public async Task<IQueryable<AppRole>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        
        List<AppRole> appRoles = await roleManager.Roles.OrderBy(
            role => role.Name
        ).ToListAsync(cancellationToken);

        return appRoles.AsQueryable();

    }
}