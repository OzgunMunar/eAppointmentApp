using eAppointment.Domain.Repositories;
using eAppointment.Domain.Users;
using eAppointment.Infrastructure.Context;
using GenericRepository;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace eAppointment.Infrastructure.Repositories;

public sealed class UserRoleRepository : Repository<AppUserRole, ApplicationDbContext>, IUserRoleRepository
{
    private readonly ApplicationDbContext _context;

    public UserRoleRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<AppUserRole>> GetAllAppUserRolesAsync(CancellationToken cancellationToken)
    {
        return await _context.UserRoles.ToListAsync(cancellationToken);
    }
}
