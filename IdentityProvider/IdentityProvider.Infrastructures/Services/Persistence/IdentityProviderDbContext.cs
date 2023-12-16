using AutoMapper.Internal;
using IdentityProvider.Application.Services;
using IdentityProvider.Domain.Entities;
using IdentityProvider.Domain.Models;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace IdentityProvider.Infrastructures.Services.Persistence
{
    public class IdentityProviderDbContext : IdentityDbContext<AppUser>, IConfigurationDbContext, IPersistedGrantDbContext, IIdentityProviderDbContext
    {
        private const string AnonymousUserId = "Anonymous";

        private readonly IServiceProvider _serviceProvider;

        public IdentityProviderDbContext(
            IServiceProvider serviceProvider,
            DbContextOptions options) : base(options)
        {
            _serviceProvider = serviceProvider;
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        public DbSet<IdentityResource> IdentityResources { get; set; }
        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<ApiScope> ApiScopes { get; set; }
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
        

        public Task<int> SaveChangesAsync()
        {
            var entities = GetChangedEntities();
            TrackNewEntities(entities);
            TrackUpdatedEntities(entities);

            return base.SaveChangesAsync();

        }

        private IEnumerable<EntityEntry<IEntity>> GetChangedEntities()
        {
            return ChangeTracker.Entries<IEntity>();
        }
        private void TrackNewEntities(IEnumerable<EntityEntry<IEntity>> entities)
        {
            var principal = _serviceProvider.GetService<ClaimsPrincipal>();

            entities
                .Where(x => x.State == EntityState.Added)
                .ForAll(x =>
                {
                    var entity = x.Entity;
                    entity.CreatedDate = DateTime.Now;
                    entity.CreatedByUserId = principal?.Identity?.Name ?? AnonymousUserId;
                });
        }
        private void TrackUpdatedEntities(IEnumerable<EntityEntry<IEntity>> entities)
        {
            var principal = _serviceProvider.GetService<ClaimsPrincipal>();

            entities
            .Where(x => x.State == EntityState.Modified)
            .ForAll(x =>
            {
                var entity = x.Entity;
                entity.ModifiedDate = DateTime.Now;
                entity.ModifiedByUserId = principal?.Identity?.Name ?? AnonymousUserId;
            });
        }
    }
}
