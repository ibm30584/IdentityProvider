using IdentityServer4.Models;

namespace IdentityProvider.Models;

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
                new Client()
                {
                    ClientId = "D59B5C69-AAE2-4D5D-8575-1078FBC18F4C",
                    ClientSecrets =  {new Secret("B3AF0DF0-7447-43E9-B2C8-9305671CBBB3".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = ["codes" ]
                }
            };
}
