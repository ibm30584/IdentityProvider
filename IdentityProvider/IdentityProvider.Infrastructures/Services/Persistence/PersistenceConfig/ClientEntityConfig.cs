using IdentityProvider.Infrastructures.Services.IdentityServer4;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Infrastructures.Services.Persistence.PersistenceConfig
{
    public class ClientEntityConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            var clientRows = MapClients(InMemoryConfigurations.Clients);
            var grantTypes = clientRows
                .Select(x => x.AllowedGrantTypes)
                .SelectMany(x => x)
                .ToList();
            builder.HasData(clientRows);            
        }

        private Client[] MapClients(global::IdentityServer4.Models.Client[] clients)
        {
            if (clients == null || clients.Length == 0)
            {
                return [];
            }

            var clientId = 1;
            return clients.Select(
                client =>
                {
                    var clientRow = new Client
                    {
                        Id = clientId,
                        Enabled = client.Enabled,
                        ClientId = client.ClientId,
                        ProtocolType = client.ProtocolType,
                        ClientSecrets = client.ClientSecrets.Select(clientClientSecret => new ClientSecret
                        {
                            Description = clientClientSecret.Description,
                            Value = clientClientSecret.Value,
                            Expiration = clientClientSecret.Expiration,
                            Type = clientClientSecret.Type
                        }).ToList(),
                        RequireClientSecret = client.RequireClientSecret,
                        ClientName = client.ClientName,
                        Description = client.Description,
                        ClientUri = client.ClientUri,
                        LogoUri = client.LogoUri,
                        RequireConsent = client.RequireConsent,
                        AllowRememberConsent = client.AllowRememberConsent,
                        AlwaysIncludeUserClaimsInIdToken = client.AlwaysIncludeUserClaimsInIdToken,
                        AllowedGrantTypes = MapAllowedGrantTypes(client.AllowedGrantTypes, clientId),
                        RequirePkce = client.RequirePkce,
                        AllowPlainTextPkce = client.AllowPlainTextPkce,
                        RequireRequestObject = client.RequireRequestObject,
                        AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
                        RedirectUris = MapRedirectUris(client.RedirectUris, clientId),
                        PostLogoutRedirectUris = MapPostLogoutRedirectUris(client.PostLogoutRedirectUris, clientId),
                        FrontChannelLogoutUri = client.FrontChannelLogoutUri,
                        FrontChannelLogoutSessionRequired = client.FrontChannelLogoutSessionRequired,
                        BackChannelLogoutUri = client.BackChannelLogoutUri,
                        BackChannelLogoutSessionRequired = client.BackChannelLogoutSessionRequired,
                        AllowOfflineAccess = client.AllowOfflineAccess,
                        AllowedScopes = MapAllowedScopes(client.AllowedScopes, clientId),
                        IdentityTokenLifetime = client.IdentityTokenLifetime,
                        AllowedIdentityTokenSigningAlgorithms = client.AllowedIdentityTokenSigningAlgorithms.ToString(),
                        AccessTokenLifetime = client.AccessTokenLifetime,
                        AuthorizationCodeLifetime = client.AuthorizationCodeLifetime,
                        ConsentLifetime = client.ConsentLifetime,
                        AbsoluteRefreshTokenLifetime = client.AbsoluteRefreshTokenLifetime,
                        SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime,
                        RefreshTokenUsage = (int)client.RefreshTokenUsage,
                        UpdateAccessTokenClaimsOnRefresh = client.UpdateAccessTokenClaimsOnRefresh,
                        RefreshTokenExpiration = (int)client.RefreshTokenExpiration,
                        AccessTokenType = (int)client.AccessTokenType,
                        EnableLocalLogin = client.EnableLocalLogin,
                        IdentityProviderRestrictions = MapIdentityProviderRestrictions(client.IdentityProviderRestrictions, clientId),
                        IncludeJwtId = client.IncludeJwtId,
                        Claims = client.Claims.Select(clientClaim => new ClientClaim
                        {
                            Type = clientClaim.Type,
                            Value = clientClaim.Value
                        }).ToList(),
                        AlwaysSendClientClaims = client.AlwaysSendClientClaims,
                        ClientClaimsPrefix = client.ClientClaimsPrefix,
                        PairWiseSubjectSalt = client.PairWiseSubjectSalt,
                        AllowedCorsOrigins = MapAllowedCorsOrigins(client.AllowedCorsOrigins, clientId),
                        Properties = MapProperties(client.Properties, clientId),
                        UserSsoLifetime = client.UserSsoLifetime,
                        UserCodeType = client.UserCodeType,
                        DeviceCodeLifetime = client.DeviceCodeLifetime
                    };
                    clientId++;
                    return clientRow;
                }).ToArray();
        }
        private List<ClientGrantType> MapAllowedGrantTypes(ICollection<string> allowedGrantTypes, int clientId)
        {
            if (allowedGrantTypes == null || allowedGrantTypes.Count == 0)
            {
                return [];
            }

            var index = 1;
            return allowedGrantTypes
                .Select(
                grantType => new ClientGrantType
                {
                    Id = index++,
                    GrantType = grantType,
                    ClientId = clientId
                })
                .ToList();
        }
        private List<ClientProperty> MapProperties(IDictionary<string, string> properties, int clientId)
        {
            if (properties == null || properties.Count == 0)
            {
                return [];
            }

            var index = 1;
            return properties
                .Select(x => new ClientProperty
                {
                    Id = index++,
                    Key = x.Key,
                    Value = x.Value,
                    ClientId = clientId
                })
                .ToList();
        }
        private List<ClientRedirectUri> MapRedirectUris(ICollection<string> redirectUris, int clientId)
        {
            if (redirectUris == null || redirectUris.Count == 0)
            {
                return [];
            }

            var index = 1;
            return redirectUris
                .Select(
                redirectUri => new ClientRedirectUri
                {
                    Id = index++,
                    RedirectUri = redirectUri,
                    ClientId = clientId
                })
                .ToList();
        }
        private List<ClientIdPRestriction> MapIdentityProviderRestrictions(ICollection<string> identityProviderRestrictions, int clientId)
        {
            if (identityProviderRestrictions == null || identityProviderRestrictions.Count == 0)
            {
                return [];
            }

            var index = 1;
            return identityProviderRestrictions
                .Select(
                provider => new ClientIdPRestriction
                {
                    Id = index++,
                    Provider = provider,
                    ClientId = clientId
                })
                .ToList();
        }
        private List<ClientCorsOrigin> MapAllowedCorsOrigins(ICollection<string> allowedCorsOrigins, int clientId)
        {
            if (allowedCorsOrigins == null || allowedCorsOrigins.Count == 0)
            {
                return [];
            }

            var index = 1;
            return allowedCorsOrigins
                .Select(
                origin => new ClientCorsOrigin
                {
                    Id = index++,
                    Origin = origin,
                    ClientId = clientId
                })
                .ToList();
        }
        private List<ClientPostLogoutRedirectUri> MapPostLogoutRedirectUris(ICollection<string> postLogoutRedirectUris, int clientId)
        {
            if (postLogoutRedirectUris == null || postLogoutRedirectUris.Count == 0)
            {
                return [];
            }

            var index = 1;
            return postLogoutRedirectUris
                .Select(
                postLogoutRedirectUri => new ClientPostLogoutRedirectUri
                {
                    Id = index++,
                    PostLogoutRedirectUri = postLogoutRedirectUri,
                    ClientId = clientId
                })
                .ToList();
        }
        private List<ClientScope> MapAllowedScopes(ICollection<string> allowedScopes, int clientId)
        {
            if (allowedScopes == null || allowedScopes.Count == 0)
            {
                return [];
            }

            var index = 1;
            return allowedScopes
                .Select(
                allowedScope => new ClientScope
                {
                    Id = index++,
                    Scope = allowedScope,
                    ClientId = clientId
                })
                .ToList();
        }

    }
}
