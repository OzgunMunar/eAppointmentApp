using eAppointment.Domain.Users;

namespace eAppointment.Application;

public static class RoleConstants
{

    public static List<AppRole> GetRoles()
    {
        List<string> roles = new()
        {
            "Admin",
            "Doctor",
            "Personnel"
        };

        return roles.Select(s => new AppRole() { Name = s }).ToList();
    }

}