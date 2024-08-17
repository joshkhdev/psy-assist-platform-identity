using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PsyAssistPlatform.AuthService.Domain;
using PsyAssistPlatform.AuthService.Persistence;

namespace PsyAssistPlatform.AuthService.DatabaseProxyService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            if (builder.Environment.IsProduction())
            {
                // ��������� Kestrel ��� ������������� HTTP
                builder.WebHost.ConfigureKestrel(options =>
                {
                    options.ListenAnyIP(5000); // ������ HTTP
                });
            }

            // �������� ��������� ������������
            builder.Services.AddControllers();

            // ��������� DbContext
            builder.Services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
            });

            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>();

            // ��������� Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            // ������������� ���� ������
            await app.InitializeDatabaseAsync(logger);

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }

}
