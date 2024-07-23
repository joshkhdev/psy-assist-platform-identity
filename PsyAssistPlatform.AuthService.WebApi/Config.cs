using IdentityServer4;
using IdentityServer4.Models;

namespace PsyAssistPlatform.AuthService.WebApi;

public class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new ApiScope("myApi.read"),
        new ApiScope("myApi.write")
    ];

    public static IEnumerable<ApiResource> ApiResources =>
        [
            new ApiResource("myApi")
            {
                Scopes = new List<string>{ "myApi.read","myApi.write", IdentityServerConstants.StandardScopes.OfflineAccess },
                ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
            }
        ];

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
                AccessTokenLifetime = 1800,
                IdentityTokenLifetime = 2592000,
                AlwaysIncludeUserClaimsInIdToken = true,
            },
            new Client
            {
                ClientId = "ro.client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "myApi.read", "myApi.write", "openid", "profile", "roles", IdentityServerConstants.StandardScopes.OfflineAccess },
                AccessTokenLifetime = 3600,
                AllowOfflineAccess = true // Enable refresh tokens
            }

        };
}
