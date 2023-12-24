using IdentityProvider.Infrastructures.Services.IdentityServer4;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Infrastructures.Services.Persistence.PersistenceConfig
{
    public class ClientGrantTypeEntityConfig : IEntityTypeConfiguration<ClientGrantType>
    {
        public void Configure(EntityTypeBuilder<ClientGrantType> builder)
        {
            builder.HasData(new ClientGrantType[]
            {
                new()
                {
                    Id = GrantTypes.
                }
            });
        }
    }
}
