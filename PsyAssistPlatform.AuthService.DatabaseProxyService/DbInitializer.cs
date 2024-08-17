using Microsoft.AspNetCore.Identity;
using PsyAssistPlatform.AuthService.Domain;
using PsyAssistPlatform.AuthService.Persistence;
using PsyAssistPlatform.AuthService.Persistence.Data;
using PsyAssistPlatform.Application.Extensions;

namespace PsyAssistPlatform.AuthService.DatabaseProxyService;

public class DbInitializer
{
    public static async Task EnsureSeedDataAsync(IServiceProvider serviceProvider, ILogger logger)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

            logger.LogInformation("Ensuring database is created...");
            await EnsureDatabaseCreatedAsync(context);

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            logger.LogInformation("Seeding roles...");
            await SeedRolesAsync(roleManager);

            logger.LogInformation("Seeding users...");
            await SeedUsersAsync(userManager);

            logger.LogInformation("Database seeding completed.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    private static async Task EnsureDatabaseCreatedAsync(AuthDbContext context)
    {
        await context.Database.EnsureCreatedAsync();
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (RoleType role in Enum.GetValues(typeof(RoleType)))
        {
            var roleName = role.ToDatabaseString();
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }

    private static async Task SeedUsersAsync(UserManager<User> userManager)
    {
        var firstUser = await userManager.FindByNameAsync(FakeDataFactory.Users.FirstOrDefault()?.Name);
        if (firstUser != null)
        {
            return;
        }

        foreach (var fakeUser in FakeDataFactory.Users)
        {
            var result = await userManager.CreateAsync(fakeUser, "Pass123$");

            if (result.Succeeded)
            {
                var res = await userManager.AddToRoleAsync(fakeUser, fakeUser.RoleType.ToDatabaseString());
            }
        }
    }
}
