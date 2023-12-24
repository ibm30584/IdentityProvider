using IdentityProvider.Infrastructures.Services.IdentityServer4;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Infrastructures.Services.Persistence.PersistenceConfig
{
    public class IdentityResourceEntityConfig : IEntityTypeConfiguration<IdentityResource>
    {
        public void Configure(EntityTypeBuilder<IdentityResource> builder)
        {
            builder.HasData(MapIdentityResources(InMemoryConfigurations.IdentityResources));
        }

        private IdentityResource[] MapIdentityResources(
            global::IdentityServer4.Models.IdentityResource[] identityResources)
        {
            return identityResources
                .Select(identityResource => new IdentityResource
                {
                    Enabled = identityResource.Enabled,
                    Name = identityResource.Name,
                    DisplayName = identityResource.DisplayName,
                    Description = identityResource.Description,
                    Required = identityResource.Required,
                    Emphasize = identityResource.Emphasize,
                    ShowInDiscoveryDocument = identityResource.ShowInDiscoveryDocument,
                    UserClaims = MapUserClaims(identityResource.UserClaims),
                    Properties = MapProperties(identityResource.Properties)
                })
                .ToArray();
        }
        private List<IdentityResourceProperty> MapProperties(IDictionary<string, string> properties)
        {
            if (properties == null || properties.Count == 0)
            {
                return [];
            }

            var index = 1;
            return properties
                .Select(
                x => new IdentityResourceProperty
                {
                    Id = index++,
                    Key = x.Key,
                    Value = x.Value
                })
                .ToList();
        }
        private List<IdentityResourceClaim> MapUserClaims(ICollection<string> userClaims)
        {
            if (userClaims == null || userClaims.Count == 0)
            {
                return [];
            }

            var index = 1;
            return userClaims
                .Select(
                userClaim => new IdentityResourceClaim
                {
                    Id = index++,
                    Type = userClaim
                })
                .ToList();
        }
    }
}
