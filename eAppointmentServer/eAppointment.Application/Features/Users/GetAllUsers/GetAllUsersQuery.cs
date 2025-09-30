using TS.MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.GetAllUsers;

public sealed record GetAllUsersQuery()
: IRequest<IQueryable<GetAllUsersQueryResponse>>;

public sealed class GetAllUsersQueryResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = default!;
    public bool IsActive { get; set; }
}
