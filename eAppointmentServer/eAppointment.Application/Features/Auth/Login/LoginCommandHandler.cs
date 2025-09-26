using eAppointment.Application.Services;
using eAppointment.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Auth.Login;

internal sealed class LoginCommandHandler(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IJwtProvider jwtProvider
) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.Users.FirstOrDefaultAsync
            (user =>
                user.Email == request.UserNameOrEmail
                ||
                user.UserName == request.UserNameOrEmail,
            cancellationToken);

        if (user is null)
        {
            return Result<LoginCommandResponse>.Failure("User could not found");
        }

        SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);

        if (signInResult.IsLockedOut)
        {
            TimeSpan? timeSpan = user.LockoutEnd - DateTime.UtcNow;
            var message = timeSpan is not null
            ? $"You entered your password 5 times in a row. Therefore, your account is locked for {Math.Ceiling(timeSpan.Value.TotalMinutes)} minutes."
            : "You entered your password 5 times in a row. Therefore, your account is locked for 5 minutes.";

            return Result<LoginCommandResponse>.Failure(message);

        }

        if (!signInResult.Succeeded)
        {
            return Result<LoginCommandResponse>.Failure("Your password is wrong.");
        }

        var token = await jwtProvider.CreateTokenAsync(user, cancellationToken);

        var response = new LoginCommandResponse
        {
            AccessToken = token
        };

        return Result<LoginCommandResponse>.Succeed(response);

    }
}