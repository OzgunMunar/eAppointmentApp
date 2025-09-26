using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Auth.Login;
public sealed record LoginCommand(
    string UserNameOrEmail,
    string Password
) : IRequest<Result<LoginCommandResponse>>;
