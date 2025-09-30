using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Users.UpdateUser;

public sealed record UserUpdateCommand(

    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password,
    Guid RoleId

) : IRequest<Result<string>>;
