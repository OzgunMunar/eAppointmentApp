using eAppointment.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace eAppointment.WebAPI;

public static class Helper
{
    public static async Task CreateUserAsync(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {

            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!userManager.Users.Any())
            {
                await userManager.CreateAsync(new()
                {
                    FirstName = "Ozgun",
                    LastName = "Munar",
                    Email = "admin@admin.com",
                    UserName = "admin",
                }, "111111");
            }

        }
    }
}