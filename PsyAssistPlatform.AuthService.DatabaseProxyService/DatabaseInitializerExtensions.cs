using PsyAssistPlatform.AuthService.Persistence;

namespace PsyAssistPlatform.AuthService.DatabaseProxyService;

public static class DatabaseInitializerExtensions
{
    public static async Task InitializeDatabaseAsync(this IApplicationBuilder app, ILogger logger)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var dbContext = services.GetRequiredService<AuthDbContext>();
                var config = services.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString("DefaultConnection");

                logger.LogInformation("Starting database initialization...");

                // Используем оптимизированный DbInitializer
                await DbInitializer.EnsureSeedDataAsync(services, logger);

                logger.LogInformation("Database initialization completed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during database initialization.");
                throw;
            }
        }
    }
}
