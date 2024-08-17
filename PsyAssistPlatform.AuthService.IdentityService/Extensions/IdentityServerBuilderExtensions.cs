using System.Security.Cryptography.X509Certificates;

namespace PsyAssistPlatform.AuthService.IdentityService.Extensions;

public static class IdentityServerBuilderExtensions
{
    public static IIdentityServerBuilder AddCustomSigningCredential(this IIdentityServerBuilder builder, IConfiguration configuration, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            builder.AddDeveloperSigningCredential();
        }
        else
        {
            var certPath = Path.Combine(environment.ContentRootPath, configuration["Certificate_Path"]);
            var certPassword = configuration["Certificate_Password"];
            var certificate = new X509Certificate2(certPath, certPassword);

            builder.AddSigningCredential(certificate);
        }

        return builder;
    }
}
