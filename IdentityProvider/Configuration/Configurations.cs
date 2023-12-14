using IdentityServer4.Models;

namespace IdentityProvider.Configuration;

public static class Configurations
{
    public static IdentityResource[] IdentityResources => [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
                ];

    public static ApiScope[] Scopes => [
                new ApiScope("codes", "Code Management"),
        new ApiScope("audit", "Audit Review"),
    ];

    public static Client[] Clients => new Client[]
            {
                new Client
                {
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientId = "D59B5C69-AAE2-4D5D-8575-1078FBC18F4C",
                    ClientSecrets =  {new Secret("B3AF0DF0-7447-43E9-B2C8-9305671CBBB3".Sha256()) },
                    AllowedScopes = ["codes" ]
                },
                new Client
                {
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientId = "E1FC0DD7-0C4E-48ED-B0D3-9CDF15546BA3",
                    AllowedScopes = ["openid", "profile", "codes"],
                    RedirectUris = ["https://localhost:6001/code-callback"],
                    FrontChannelLogoutUri = "https://localhost:6001/logout-callback",
                    PostLogoutRedirectUris = ["https://localhost:6001/index"],
                    AllowedCorsOrigins = ["https://localhost:6001"],
                    RequirePkce = true
                }
            };
}
