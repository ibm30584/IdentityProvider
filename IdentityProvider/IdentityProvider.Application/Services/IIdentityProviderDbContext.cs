using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider.Application.Services
{
    public interface IIdentityProviderDbContext
    {
        DbSet<ApiResource> ApiResources { get; set; }
        DbSet<ApiScope> ApiScopes { get; set; }
        DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
        DbSet<IdentityResource> IdentityResources { get; set; }
        DbSet<PersistedGrant> PersistedGrants { get; set; }

        Task<int> SaveChangesAsync();
    }
}