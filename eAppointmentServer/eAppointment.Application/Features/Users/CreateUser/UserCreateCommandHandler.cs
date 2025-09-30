using eAppointment.Domain.Users;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.CreateUser;

internal sealed class UserCreateCommandHandler(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    IUnitOfWork unitOfWork
) :
IRequestHandler<UserCreateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {

        AppUser? appUser = await userManager.Users.Where(user => user.Email == request.Email).FirstOrDefaultAsync(cancellationToken);

        if (appUser != null)
        {
            return Result<string>.Failure("There is already a user with the same email address.");
        }

        AppUser newUser = new()
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.UserName
        };

        var identityResult = await userManager.CreateAsync(newUser, request.Password);

        if (!identityResult.Succeeded)
        {
            return Result<string>.Failure("Problem with saving new user.");
        }

        var role = await roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role != null)
        {
            await userManager.AddToRoleAsync(newUser, role.Name!);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("User created successfully");

    }
    
}