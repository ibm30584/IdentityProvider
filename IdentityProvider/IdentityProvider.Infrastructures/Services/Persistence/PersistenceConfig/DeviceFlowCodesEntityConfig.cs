using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProvider.Infrastructures.Services.Persistence.PersistenceConfig
{
    public class DeviceFlowCodesEntityConfig : IEntityTypeConfiguration<DeviceFlowCodes>
    {
        public void Configure(EntityTypeBuilder<DeviceFlowCodes> builder)
        {
            builder.HasNoKey();
        }
    }
}
