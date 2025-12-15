using HotelBooking.Infrastructure.Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.Infrastructure.Authentication;

public class AuthorizationSeeder
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AuthorizationDbContext>();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var roleName in new[] { RoleNames.ADMIN, RoleNames.USER })
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                if (!roleResult.Succeeded)
                {
                    throw new Exception($"Failed to create role {roleName}: " +
                                        string.Join("; ", roleResult.Errors.Select(e => e.Description)));
                }
            }
        }

        var admin = await userManager.FindByNameAsync("string");
        if (admin == null)
        {
            admin = new ApplicationUser { UserName = "string", Email = "admin@example.com" };

            var createResult = await userManager.CreateAsync(admin, "string");
            if (!createResult.Succeeded)
            {
                throw new Exception("Failed to create admin user: " +
                                    string.Join("; ", createResult.Errors.Select(e => e.Description)));
            }
        }

        if (!await userManager.IsInRoleAsync(admin, RoleNames.ADMIN))
        {
            var addRoleResult = await userManager.AddToRoleAsync(admin, RoleNames.ADMIN);
            if (!addRoleResult.Succeeded)
            {
                throw new Exception("Failed to assign Admin role: " +
                                    string.Join("; ", addRoleResult.Errors.Select(e => e.Description)));
            }
        }
    }
}