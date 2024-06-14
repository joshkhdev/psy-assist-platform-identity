using IdentityServer4;
using IdentityServer4.Models;

namespace AuthService;

public class Config
{
    // Identity resources are data like user ID, name, email, profile picture, etc.
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
    ];

    // API scopes are the permissions that the client application is requesting to access the API.
    public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
    {
        new ApiScope("myApi.read"),
        new ApiScope("myApi.write")
    };

    // API resources are the actual APIs that the client application is requesting to access.
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("myApi")
            {
                Scopes = new List<string>{ "myApi.read","myApi.write", IdentityServerConstants.StandardScopes.OfflineAccess },
                ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
            }
        };

    // Clients are the applications that will be requesting the API.
    public static IEnumerable<Client> Clients =>
        new Client[]
        {
              new Client
                    {
                        ClientId = "cwm.client",
                        ClientName = "Client Credentials Client",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets = { new Secret("secret".Sha256()) },
                        AllowedScopes = { "myApi.read" },
                        AccessTokenLifetime = 60,
                        IdentityTokenLifetime = 60,
                        AlwaysIncludeUserClaimsInIdToken = true,
                    }
        };
}
