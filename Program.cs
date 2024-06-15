using AuthService;
using IdentityServer4.Validation;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

var certPath = builder.Configuration["Certificate:Path"];
var certPassword = builder.Configuration["Certificate:Password"];
var certificate = new X509Certificate2(certPath, certPassword);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(443, listenOptions =>
    {
        listenOptions.UseHttps(certPath, certPassword); // HTTPS
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


builder.Services.AddIdentityServer()
    .AddSigningCredential(new X509Certificate2(certPath, certPassword))
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes);

builder.Services.AddTransient<ICustomTokenRequestValidator, CustomTokenRequestValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("CorsPolicy");

app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();
app.Run();
