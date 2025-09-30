using eAppointment.Domain.Users;
using GenericRepository;

namespace eAppointment.Domain.Repositories;
public interface IUserRoleRepository : IRepository<AppUserRole>
{
    Task<List<AppUserRole>> GetAllAppUserRolesAsync(CancellationToken cancellationToken);
}