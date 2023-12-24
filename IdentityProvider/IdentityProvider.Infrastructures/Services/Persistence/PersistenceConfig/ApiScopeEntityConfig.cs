using IdentityProvider.Infrastructures.Services.IdentityServer4;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Infrastructures.Services.Persistence.PersistenceConfig
{
    public class ApiScopeEntityConfig : IEntityTypeConfiguration<ApiScope>
    {
        public void Configure(EntityTypeBuilder<ApiScope> builder)
        {
            builder.HasData(MapApiScopes(InMemoryConfigurations.Scopes));
        }

        private ApiScope[] MapApiScopes(global::IdentityServer4.Models.ApiScope[] scopes)
        {
            if (scopes == null || scopes.Length == 0)
            {
                return [];
            }

            var index = 1;
            return scopes.Select(scope => new ApiScope
            {
                Id = index++,
                Enabled = scope.Enabled,
                Name = scope.Name,
                DisplayName = scope.DisplayName,
                Description = scope.Description,
                Required = scope.Required,
                Emphasize = scope.Emphasize,
                ShowInDiscoveryDocument = scope.ShowInDiscoveryDocument,
                UserClaims = MapUserClaims(scope.UserClaims),
                Properties = MapProperties(scope.Properties)
            }).ToArray();
        }
        private List<ApiScopeClaim> MapUserClaims(ICollection<string> userClaims)
        {
            if (userClaims == null || userClaims.Count == 0)
            {
                return [];
            }

            var index = 1;
            return userClaims
                .Select(
                userClaim => new ApiScopeClaim
                {
                    Id = index++,
                    Type = userClaim
                })
                .ToList();
        }
        private List<ApiScopeProperty> MapProperties(IDictionary<string, string> properties)
        {
            if (properties == null || properties.Count == 0)
            {
                return [];
            }

            var index = 1;
            return properties
                .Select(
                x => new ApiScopeProperty
                {
                    Id = index++,
                    Key = x.Key,
                    Value = x.Value
                })
                .ToList();
        }
    }
}
