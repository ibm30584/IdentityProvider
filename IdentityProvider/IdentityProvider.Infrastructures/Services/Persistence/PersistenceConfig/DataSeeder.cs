using AutoMapper.Internal;
using IdentityProvider.Infrastructures.Services.IdentityServer4;
using IdentityServer4.EntityFramework.Entities; 
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider.Infrastructures.Services.Persistence.PersistenceConfig
{
    public class DataSeeder
    {
        //private static int _allowedGrantTypeIndex = 1;
        //private static int _allowedScopeIndex = 1;
        //private static int _postLogoutRedirectUriIndex = 1;
        //private static int _allowedCorsOriginIndex = 1;
        //private static int _identityProviderRestrictionIndex = 1;
        //private static int _redirectUriIndex = 1;
        //private static int _clientSecret = 1;
        //private static int _clientPropertyIndex = 1;

        private readonly List<Client> _clientRows = [];
        private readonly List<ClientSecret> _secretRows = [];
        private readonly List<ClientClaim> _claimRows = [];
        private readonly List<ClientGrantType> _grantTypeRows = [];
        private readonly List<ClientProperty> _propertyRows = [];
        private readonly List<ClientRedirectUri> _redirectUriRows = [];
        private readonly List<ClientIdPRestriction> _idPRestrictionRows = [];
        private readonly List<ClientCorsOrigin> _corsOriginRows = [];
        private readonly List<ClientPostLogoutRedirectUri> _postLogoutRedirectUriRows = [];
        private readonly List<ClientScope> _scopeRows = [];

        public void SeedData(ModelBuilder builder)
        {
            PrepareData(InMemoryConfigurations.Clients);

            builder.Entity<Client>().HasData(_clientRows);
            builder.Entity<ClientSecret>().HasData(_secretRows);
            builder.Entity<ClientClaim>().HasData(_claimRows);
            builder.Entity<ClientGrantType>().HasData(_grantTypeRows);
            builder.Entity<ClientProperty> ().HasData(_propertyRows);
            builder.Entity<ClientRedirectUri> ().HasData(_redirectUriRows);
            builder.Entity<ClientIdPRestriction>().HasData(_idPRestrictionRows);
            builder.Entity<ClientCorsOrigin>().HasData(_corsOriginRows);
            builder.Entity<ClientPostLogoutRedirectUri>().HasData(_postLogoutRedirectUriRows);
            builder.Entity<ClientScope>().HasData(_scopeRows);

            //var clientRows = MapClients(InMemoryConfigurations.Clients);

            //var clientGrantTypes = clientRows
            //    .Select(x => x.AllowedGrantTypes)
            //    .SelectMany(x => x)
            //    .ToList();

            //builder.Entity<ClientGrantType>().HasData(clientGrantTypes);

            //var clientProperties = clientRows
            //    .Select(x => x.Properties)
            //    .SelectMany(x => x)
            //    .ToList();
            //builder.Entity<ClientProperty>().HasData(clientProperties);

            //var redirectUris = clientRows
            //    .Select(x => x.RedirectUris)
            //    .SelectMany(x => x)
            //    .ToList();
            //builder.Entity<ClientRedirectUri>().HasData(redirectUris);

            //var identityProviderRestrictions = clientRows
            //    .Select(x => x.IdentityProviderRestrictions)
            //    .SelectMany(x => x)
            //    .ToList();
            //builder.Entity<ClientIdPRestriction>().HasData(identityProviderRestrictions);

            //var allowedCorsOrigins = clientRows
            //    .Select(x => x.AllowedCorsOrigins)
            //    .SelectMany(x => x)
            //    .ToList();
            //builder.Entity<ClientCorsOrigin>().HasData(allowedCorsOrigins);

            //var postLogoutRedirectUris = clientRows
            //    .Select(x => x.PostLogoutRedirectUris)
            //    .SelectMany(x => x)
            //    .ToList();
            //builder.Entity<ClientPostLogoutRedirectUri>().HasData(postLogoutRedirectUris);

            //var allowedScopes = clientRows
            //   .Select(x => x.AllowedScopes)
            //   .SelectMany(x => x)
            //   .ToList();
            //builder.Entity<ClientScope>().HasData(allowedScopes);

            //var clientSecrets = clientRows
            //   .Select(x => x.ClientSecrets)
            //   .SelectMany(x => x)
            //   .ToList();
            //builder.Entity<ClientSecret>().HasData(clientSecrets);

            //clientRows.ForAll(x =>
            //{
            //    x.AllowedGrantTypes = null;
            //    x.Properties = null;
            //    x.RedirectUris = null;
            //    x.IdentityProviderRestrictions = null;
            //    x.AllowedCorsOrigins = null;
            //    x.PostLogoutRedirectUris = null;
            //    x.AllowedScopes = null;
            //    x.ClientSecrets = null;
            //});

            //builder.Entity<Client>().HasData(clientRows);
        }
        private void PrepareData(global::IdentityServer4.Models.Client[] clients)
        {
            if (clients == null || clients.Length == 0)
            {
                return;
            }

            foreach (var client in clients)
            {
                var clientRow = new Client
                {
                    Id = _clientRows.Count + 1,
                    Enabled = client.Enabled,
                    ClientId = client.ClientId,
                    ProtocolType = client.ProtocolType,
                    RequireClientSecret = client.RequireClientSecret,
                    ClientName = client.ClientName,
                    Description = client.Description,
                    ClientUri = client.ClientUri,
                    LogoUri = client.LogoUri,
                    RequireConsent = client.RequireConsent,
                    AllowRememberConsent = client.AllowRememberConsent,
                    AlwaysIncludeUserClaimsInIdToken = client.AlwaysIncludeUserClaimsInIdToken,
                    RequirePkce = client.RequirePkce,
                    AllowPlainTextPkce = client.AllowPlainTextPkce,
                    RequireRequestObject = client.RequireRequestObject,
                    AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
                    FrontChannelLogoutUri = client.FrontChannelLogoutUri,
                    FrontChannelLogoutSessionRequired = client.FrontChannelLogoutSessionRequired,
                    BackChannelLogoutUri = client.BackChannelLogoutUri,
                    BackChannelLogoutSessionRequired = client.BackChannelLogoutSessionRequired,
                    AllowOfflineAccess = client.AllowOfflineAccess,
                    IdentityTokenLifetime = client.IdentityTokenLifetime,
                    AllowedIdentityTokenSigningAlgorithms = string.Join(',', client.AllowedIdentityTokenSigningAlgorithms),
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
                    IncludeJwtId = client.IncludeJwtId,                  
                    AlwaysSendClientClaims = client.AlwaysSendClientClaims,
                    ClientClaimsPrefix = client.ClientClaimsPrefix,
                    PairWiseSubjectSalt = client.PairWiseSubjectSalt,
                    UserSsoLifetime = client.UserSsoLifetime,
                    UserCodeType = client.UserCodeType,
                    DeviceCodeLifetime = client.DeviceCodeLifetime
                };
                _clientRows.Add(clientRow);

                if (client.ClientSecrets != null && client.ClientSecrets.Count != 0)
                {
                    foreach (var clientSecret in client.ClientSecrets)
                    {
                        var _secretRow = new ClientSecret
                        {
                            Id = _secretRows.Count + 1,
                            ClientId = clientRow.Id,
                            Description = clientSecret.Description,
                            Value = clientSecret.Value,
                            Expiration = clientSecret.Expiration,
                            Type = clientSecret.Type
                        };
                        _secretRows.Add(_secretRow);
                    }
                }
                if (client.AllowedGrantTypes != null && client.AllowedGrantTypes.Count != 0)
                {
                    foreach (var grantType in client.AllowedGrantTypes)
                    {
                        var grantTypeRow = new ClientGrantType
                        {
                            Id = _grantTypeRows.Count + 1,
                            GrantType = grantType,
                            ClientId = clientRow.Id
                        };
                        _grantTypeRows.Add(grantTypeRow);
                    }
                }
                if (client.Claims != null && client.Claims.Count != 0)
                {
                    foreach (var claim in client.Claims)
                    {
                        var claimRow = new ClientClaim
                        {
                            Id = _claimRows.Count + 1,
                            Type = claim.Type,
                            Value = claim.Value,
                            ClientId = clientRow.Id
                        };
                        _claimRows.Add(claimRow);
                    }
                }
                if (client.Properties != null && client.Properties.Count != 0)
                {
                    foreach (var prop in client.Properties)
                    {
                        var propertyRow = new ClientProperty
                        {
                            Id = _propertyRows.Count + 1,
                            Key = prop.Key,
                            Value = prop.Value,
                            ClientId = clientRow.Id
                        };
                        _propertyRows.Add(propertyRow);
                    }
                }
                if (client.RedirectUris != null && client.RedirectUris.Count != 0)
                {
                    foreach (var redirectUri in client.RedirectUris)
                    {
                        var redirectUriRow = new ClientRedirectUri
                        {
                            Id = _redirectUriRows.Count + 1,
                            RedirectUri = redirectUri,
                            ClientId = clientRow.Id
                        };
                        _redirectUriRows.Add(redirectUriRow);
                    }
                }
                if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Count != 0)
                {
                    foreach (var identityProviderRestriction in client.IdentityProviderRestrictions)
                    {
                        var idPRestrictionRow = new ClientIdPRestriction
                        {
                            Id = _idPRestrictionRows.Count + 1,
                            Provider = identityProviderRestriction,
                            ClientId = clientRow.Id
                        };
                        _idPRestrictionRows.Add(idPRestrictionRow);
                    }
                }
                if (client.AllowedCorsOrigins != null && client.AllowedCorsOrigins.Count != 0)
                {
                    foreach (var origin in client.AllowedCorsOrigins)
                    {
                        var corsOriginRow = new ClientCorsOrigin
                        {
                            Id = _corsOriginRows.Count + 1,
                            Origin = origin,
                            ClientId = clientRow.Id
                        };
                        _corsOriginRows.Add(corsOriginRow);
                    }
                }
                if (client.PostLogoutRedirectUris != null && client.PostLogoutRedirectUris.Count != 0)
                {
                    foreach (var postLogoutRedirectUri in client.PostLogoutRedirectUris)
                    {
                        var postLogoutRedirectUriRow = new ClientPostLogoutRedirectUri
                        {
                            Id = _postLogoutRedirectUriRows.Count + 1,
                            PostLogoutRedirectUri = postLogoutRedirectUri,
                            ClientId = clientRow.Id
                        };
                        _postLogoutRedirectUriRows.Add(postLogoutRedirectUriRow);
                    }
                }
                if (client.AllowedScopes != null && client.AllowedScopes.Count != 0)
                {
                    foreach (var scope in client.AllowedScopes)
                    {
                        var scopeRow = new ClientScope
                        {
                            Id = _scopeRows.Count + 1,
                            Scope = scope,
                            ClientId = clientRow.Id
                        };
                        _scopeRows.Add(scopeRow);
                    }
                }
            }

                //var clientId = 1;
                //return clients
                //    .Select(client =>
                //    {
                //        var clientRow = new Client
                //        {
                //            Id = clientId,
                //            Enabled = client.Enabled,
                //            ClientId = client.ClientId,
                //            ProtocolType = client.ProtocolType,
                //            ClientSecrets = client.ClientSecrets.Select(clientClientSecret => new ClientSecret
                //            {
                //                Id= _clientSecret++,
                //                ClientId = clientId,
                //                Description = clientClientSecret.Description,
                //                Value = clientClientSecret.Value,
                //                Expiration = clientClientSecret.Expiration,
                //                Type = clientClientSecret.Type
                //            }).ToList(),
                //            RequireClientSecret = client.RequireClientSecret,
                //            ClientName = client.ClientName,
                //            Description = client.Description,
                //            ClientUri = client.ClientUri,
                //            LogoUri = client.LogoUri,
                //            RequireConsent = client.RequireConsent,
                //            AllowRememberConsent = client.AllowRememberConsent,
                //            AlwaysIncludeUserClaimsInIdToken = client.AlwaysIncludeUserClaimsInIdToken,
                //            AllowedGrantTypes = MapAllowedGrantTypes(client.AllowedGrantTypes, clientId),
                //            RequirePkce = client.RequirePkce,
                //            AllowPlainTextPkce = client.AllowPlainTextPkce,
                //            RequireRequestObject = client.RequireRequestObject,
                //            AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
                //            RedirectUris = MapRedirectUris(client.RedirectUris, clientId),
                //            PostLogoutRedirectUris = MapPostLogoutRedirectUris(client.PostLogoutRedirectUris, clientId),
                //            FrontChannelLogoutUri = client.FrontChannelLogoutUri,
                //            FrontChannelLogoutSessionRequired = client.FrontChannelLogoutSessionRequired,
                //            BackChannelLogoutUri = client.BackChannelLogoutUri,
                //            BackChannelLogoutSessionRequired = client.BackChannelLogoutSessionRequired,
                //            AllowOfflineAccess = client.AllowOfflineAccess,
                //            AllowedScopes = MapAllowedScopes(client.AllowedScopes, clientId),
                //            IdentityTokenLifetime = client.IdentityTokenLifetime,
                //            AllowedIdentityTokenSigningAlgorithms = client.AllowedIdentityTokenSigningAlgorithms.ToString(),
                //            AccessTokenLifetime = client.AccessTokenLifetime,
                //            AuthorizationCodeLifetime = client.AuthorizationCodeLifetime,
                //            ConsentLifetime = client.ConsentLifetime,
                //            AbsoluteRefreshTokenLifetime = client.AbsoluteRefreshTokenLifetime,
                //            SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime,
                //            RefreshTokenUsage = (int)client.RefreshTokenUsage,
                //            UpdateAccessTokenClaimsOnRefresh = client.UpdateAccessTokenClaimsOnRefresh,
                //            RefreshTokenExpiration = (int)client.RefreshTokenExpiration,
                //            AccessTokenType = (int)client.AccessTokenType,
                //            EnableLocalLogin = client.EnableLocalLogin,
                //            IdentityProviderRestrictions = MapIdentityProviderRestrictions(client.IdentityProviderRestrictions, clientId),
                //            IncludeJwtId = client.IncludeJwtId,
                //            Claims = client.Claims.Select(clientClaim => new ClientClaim
                //            {
                //                ClientId = clientId,
                //                Type = clientClaim.Type,
                //                Value = clientClaim.Value
                //            }).ToList(),
                //            AlwaysSendClientClaims = client.AlwaysSendClientClaims,
                //            ClientClaimsPrefix = client.ClientClaimsPrefix,
                //            PairWiseSubjectSalt = client.PairWiseSubjectSalt,
                //            AllowedCorsOrigins = MapAllowedCorsOrigins(client.AllowedCorsOrigins, clientId),
                //            Properties = MapProperties(client.Properties, clientId),
                //            UserSsoLifetime = client.UserSsoLifetime,
                //            UserCodeType = client.UserCodeType,
                //            DeviceCodeLifetime = client.DeviceCodeLifetime
                //        };
                //        clientId++;
                //        return clientRow;
                //    })
                //    .ToArray();
            }
        //private static List<ClientGrantType> MapAllowedGrantTypes(ICollection<string> allowedGrantTypes, int clientId)
        //{
        //    if (allowedGrantTypes == null || allowedGrantTypes.Count == 0)
        //    {
        //        return [];
        //    }
            
        //    return allowedGrantTypes
        //        .Select(
        //        grantType => new ClientGrantType
        //        {
        //            Id = _allowedGrantTypeIndex++,
        //            GrantType = grantType,
        //            ClientId = clientId
        //        })
        //        .ToList();
        //}
        //private static List<ClientProperty> MapProperties(IDictionary<string, string> properties, int clientId)
        //{
        //    if (properties == null || properties.Count == 0)
        //    {
        //        return [];
        //    }

            
        //    return properties
        //        .Select(x => new ClientProperty
        //        {
        //            Id = _clientPropertyIndex++,
        //            Key = x.Key,
        //            Value = x.Value,
        //            ClientId = clientId
        //        })
        //        .ToList();
        //}
        //private static List<ClientRedirectUri> MapRedirectUris(ICollection<string> redirectUris, int clientId)
        //{
        //    if (redirectUris == null || redirectUris.Count == 0)
        //    {
        //        return [];
        //    }

            
        //    return redirectUris
        //        .Select(
        //        redirectUri => new ClientRedirectUri
        //        {
        //            Id = _redirectUriIndex++,
        //            RedirectUri = redirectUri,
        //            ClientId = clientId
        //        })
        //        .ToList();
        //}
        //private static List<ClientIdPRestriction> MapIdentityProviderRestrictions(ICollection<string> identityProviderRestrictions, int clientId)
        //{
        //    if (identityProviderRestrictions == null || identityProviderRestrictions.Count == 0)
        //    {
        //        return [];
        //    }

            
        //    return identityProviderRestrictions
        //        .Select(
        //        provider => new ClientIdPRestriction
        //        {
        //            Id = _identityProviderRestrictionIndex++,
        //            Provider = provider,
        //            ClientId = clientId
        //        })
        //        .ToList();
        //}
        //private static List<ClientCorsOrigin> MapAllowedCorsOrigins(ICollection<string> allowedCorsOrigins, int clientId)
        //{
        //    if (allowedCorsOrigins == null || allowedCorsOrigins.Count == 0)
        //    {
        //        return [];
        //    }

            
        //    return allowedCorsOrigins
        //        .Select(
        //        origin => new ClientCorsOrigin
        //        {
        //            Id = _allowedCorsOriginIndex++,
        //            Origin = origin,
        //            ClientId = clientId
        //        })
        //        .ToList();
        //}
        //private static List<ClientPostLogoutRedirectUri> MapPostLogoutRedirectUris(ICollection<string> postLogoutRedirectUris, int clientId)
        //{
        //    if (postLogoutRedirectUris == null || postLogoutRedirectUris.Count == 0)
        //    {
        //        return [];
        //    }

            
        //    return postLogoutRedirectUris
        //        .Select(
        //        postLogoutRedirectUri => {
        //            var clientPostLogoutRedirectUri = new ClientPostLogoutRedirectUri
        //            {
        //                Id = _postLogoutRedirectUriIndex,
        //                PostLogoutRedirectUri = postLogoutRedirectUri,
        //                ClientId = clientId
        //            };
        //            _postLogoutRedirectUriIndex++;
        //            return clientPostLogoutRedirectUri;
        //        })
        //        .ToList();
        //}
        //private static List<ClientScope> MapAllowedScopes(ICollection<string> allowedScopes, int clientId)
        //{
        //    if (allowedScopes == null || allowedScopes.Count == 0)
        //    {
        //        return [];
        //    }

        //    return allowedScopes
        //        .Select(
        //        allowedScope => new ClientScope
        //        {
        //            Id = _allowedScopeIndex++,
        //            Scope = allowedScope,
        //            ClientId = clientId
        //        })
        //        .ToList();
        //}
    }
}
