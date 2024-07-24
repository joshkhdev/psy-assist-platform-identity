using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PsyAssistPlatform.AuthService.Application.Interfaces.Service;
using PsyAssistPlatform.AuthService.Application.Services;
using PsyAssistPlatform.AuthService.Domain;
using PsyAssistPlatform.AuthService.Persistence;
using PsyAssistPlatform.AuthService.WebApi;
using System.Security.Cryptography.X509Certificates;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();
});

var certPath = Path.Combine(builder.Environment.ContentRootPath, builder.Configuration["Certificate_Path"]);
var certPassword = builder.Configuration["Certificate_Password"];
var certificate = new X509Certificate2(certPath, certPassword);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer(x =>
{
    x.IssuerUri = "https://psy-authservice.containers.cloud.ru";
})
     .AddSigningCredential(certificate)
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
        ValidIssuer = "https://psy-authservice.containers.cloud.ru", // Use the issuer from the certificate
        ValidAudience = "myApi", // Specify your audience
        IssuerSigningKey = new X509SecurityKey(certificate)
    };
});

builder.Services.AddTransient<ICustomTokenRequestValidator, CustomTokenRequestValidator>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRoleService, RoleService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    //await dbContext.Database.MigrateAsync();

    // Seed the database
    var config = app.Services.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("DefaultConnection");
    await DbInitializer.EnsureSeedData(connectionString);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseIdentityServer();
app.UseAuthentication(); // Add this line
app.UseAuthorization();

app.MapControllers();

app.Run();
