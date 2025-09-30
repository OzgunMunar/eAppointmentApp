using eAppointment.Domain.Users;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Users.UpdateUser;

internal sealed class UserUpdateCommandHandler(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    IUnitOfWork unitOfWork
)
: IRequestHandler<UserUpdateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {

        AppUser? appUser = await userManager.Users
                    .Where(user => user.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);

        if (appUser is null)
        {
            return Result<string>.Failure("User could not found.");
        }

        appUser.FirstName = request.FirstName;
        appUser.LastName = request.LastName;
        appUser.Email = request.Email;
        appUser.UserName = request.UserName;

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            await userManager.RemovePasswordAsync(appUser);
            await userManager.AddPasswordAsync(appUser, request.Password);
        }


        var currentRoles = await userManager.GetRolesAsync(appUser);

        if (currentRoles.Any())
        {
            await userManager.RemoveFromRolesAsync(appUser, currentRoles);
        }

        var newRole = await roleManager.FindByIdAsync(request.RoleId.ToString());

        if (newRole is null)
        {
            return Result<string>.Failure("Role could not be found.");
        }

        await userManager.AddToRoleAsync(appUser, newRole.Name!);
        await userManager.UpdateAsync(appUser);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("User is updated successfully.");

    }
}