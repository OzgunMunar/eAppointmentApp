using eAppointment.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace eAppointment.WebAPI;

public static class ExtensionsMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!userManager.Users.Any(p => p.UserName == "admin"))
            {
                AppUser user = new()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "Ozgun",
                    LastName = "Munar",
                    EmailConfirmed = true,
                    CreatedAt = DateTimeOffset.Now,
                };

                user.CreatedUserId = user.Id;

                userManager.CreateAsync(user, "111111").Wait();


            }
        }
    }
}