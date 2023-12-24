using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Infrastructures.Services.Persistence.PersistenceConfig
{
    public class ApiResourceEntityConfig : IEntityTypeConfiguration<ApiResource>
    {
        public void Configure(EntityTypeBuilder<ApiResource> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
