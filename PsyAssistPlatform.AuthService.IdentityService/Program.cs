using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PsyAssistPlatform.AuthService.Application.Interfaces.Service;
using PsyAssistPlatform.AuthService.Application.Services;
using PsyAssistPlatform.AuthService.Domain;
using PsyAssistPlatform.AuthService.IdentityService.Extensions;
using PsyAssistPlatform.AuthService.IdentityService.Middleware;
using PsyAssistPlatform.AuthService.IdentityService.Model.User;
using System.Security.Cryptography.X509Certificates;

namespace PsyAssistPlatform.AuthService.IdentityService;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (builder.Environment.IsProduction())
        {
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(5001);
            });
        }

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        // Add services to the container.
        builder.Services.AddAuthorization();

        // РегистрацияtpClient для ApiClient
        builder.Services.AddHttpClient<ApiClient>(client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["DatabaseServiceUri"]);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        })
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
        });

        // Регистрация кастомных реализаций IUserStore, IUserPasswordStore и IRoleStore
        builder.Services.AddScoped<IUserStore<User>, CustomUserStore>();
        builder.Services.AddScoped<IUserPasswordStore<User>, CustomUserStore>();
        builder.Services.AddScoped<IRoleStore<IdentityRole>, CustomRoleStore>();

        builder.Services.AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders();

        builder.Services.AddIdentityServer(x =>
        {
            x.IssuerUri = builder.Configuration["IssuerUri"];
        })
            .AddCustomSigningCredential(builder.Configuration, builder.Environment)
            .AddInMemoryClients(Config.Clients)
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddAspNetIdentity<User>()
            .AddProfileService<CustomProfileService>(); // Add custom profile service

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["IssuerUri"],
                    ValidAudience = "myApi",
                    IssuerSigningKey = GetSigningKey(builder.Configuration, builder.Environment)
                };
            });

        // Настройка Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCustomSwaggerGen();

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserRequestValidator>();

        builder.Services.AddTransient<ICustomTokenRequestValidator, CustomTokenRequestValidator>();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IRoleService, RoleService>();

        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        var app = builder.Build();

        //if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseForwardedProto();
        app.UseHttpsEnforcer(".internal", "");

        app.UseForwardedHeaders();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors("CorsPolicy");

        app.UseIdentityServer();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }

    public static SecurityKey GetSigningKey(IConfiguration configuration, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            var filename = Path.Combine(Directory.GetCurrentDirectory(), "tempkey.jwk");
            var json = File.ReadAllText(filename);
            var jwk = new JsonWebKey(json);
            return jwk;
        }
        else
        {
            // Для Production среды используем реальный сертификат
            var certPath = configuration["Certificate_Path"];
            var certPassword = configuration["Certificate_Password"];
            var certificate = new X509Certificate2(certPath, certPassword);

            return new X509SecurityKey(certificate);
        }
    }
}
