using Magic.Infrastructure.Data.Identity.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Magic.Infrastructure.Data.Extensions
{
    public static class IdentitySeeder
    {
        public static async Task SeedIdentityAsync(this WebApplication app)
        {
            // Resolve the required services
            using var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ConsumerUser>>();

            // Define roles to seed
            var roles = new[] { "Admin", "User", "Manager" };

            // Seed roles
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
                }
            }

            // Define admin user to seed
            var adminEmail = "admin@example.com";
            var adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ConsumerUser
                {
                    UserName = adminEmail,
                    FullName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Define test user to seed
            var testUserEmail = "user@example.com";
            var testUserPassword = "User@123";

            var testUser = await userManager.FindByEmailAsync(testUserEmail);
            if (testUser == null)
            {
                testUser = new ConsumerUser
                {
                    UserName = testUserEmail,
                    FullName = testUserEmail,
                    Email = testUserEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(testUser, testUserPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(testUser, "User");
                }
            }
        }
    }
}
