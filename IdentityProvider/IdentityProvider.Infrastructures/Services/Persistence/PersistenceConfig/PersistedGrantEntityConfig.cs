using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Infrastructures.Services.Persistence.PersistenceConfig
{
    public class PersistedGrantEntityConfig : IEntityTypeConfiguration<PersistedGrant>
    {
        public void Configure(EntityTypeBuilder<PersistedGrant> builder)
        {
            builder.HasKey(x => x.Key);
        }
    }
}
