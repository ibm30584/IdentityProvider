using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Infrastructures.Services.Persistence.PersistenceConfig
{
    public class ClientCorsOriginEntityConfig : IEntityTypeConfiguration<ClientCorsOrigin>
    {
        public void Configure(EntityTypeBuilder<ClientCorsOrigin> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
