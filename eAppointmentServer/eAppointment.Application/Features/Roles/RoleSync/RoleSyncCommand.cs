using eAppointment.Application;
using eAppointment.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Features.Roles.RoleSync;

public sealed record RoleSyncCommand(): IRequest<Result<string>>;

internal sealed class RoleSyncCommandHandler(
    RoleManager<AppRole> roleManager
) : IRequestHandler<RoleSyncCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RoleSyncCommand request, CancellationToken cancellationToken)
    {

        List<AppRole> currentRoles = await roleManager.Roles.ToListAsync(cancellationToken);

        List<AppRole> staticRoles = RoleConstants.GetRoles();

        foreach (var role in currentRoles)
        {

            if (!staticRoles.Any(p => p.Name == role.Name))
            {
                await roleManager.DeleteAsync(role);
            }

        }

        foreach (var role in staticRoles)
        {

            if (!currentRoles.Any(p => p.Name == role.Name))
            {
                await roleManager.CreateAsync(role);
            }

        }

        return Result<string>.Succeed("Sync is successful");

    }
}