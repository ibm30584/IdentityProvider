using IdentityProvider.Application.Models;
using IdentityProvider.Application.Services;
using IdentityProvider.Domain.Entities;
using IdentityProvider.Domain.Models;
using IdentityProvider.Infrastructures.Services.Persistence.PersistenceConfig;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace IdentityProvider.Infrastructures.Services.Persistence
{
    public class IdentityProviderDbContext(
        IServiceProvider serviceProvider,
        IOptions<IdentityServerConfig> optionsIdentityServerConfig,
        DbContextOptions options) :
        IdentityDbContext<AppUser>(options),
        IConfigurationDbContext,
        IPersistedGrantDbContext,
        IIdentityProviderDbContext
    {
        private const string AnonymousUserId = "Anonymous";
        private readonly IdentityServerConfig _identityServerConfig = optionsIdentityServerConfig.Value ?? throw new ArgumentNullException("optionsIdentityServerConfig");

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        public DbSet<IdentityResource> IdentityResources { get; set; }
        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<ApiScope> ApiScopes { get; set; }
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
        public DbSet<ClientGrantType> ClientGrantTypes { get; set; }


        public DbSet<ClientProperty> ClientProperties { get; set; }
        public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }
        public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }
        public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
        public DbSet<ClientScope> ClientScopes { get; set; }
        public DbSet<ClientClaim> ClientClaims { get; set; }


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
            var principal = serviceProvider.GetService<ClaimsPrincipal>();

            entities
                .Where(x => x.State == EntityState.Added)
                .ToList()
                .ForEach(x =>
                {
                    var entity = x.Entity;
                    entity.CreatedDate = DateTime.Now;
                    entity.CreatedByUserId = principal?.Identity?.Name ?? AnonymousUserId;
                });
        }
        private void TrackUpdatedEntities(IEnumerable<EntityEntry<IEntity>> entities)
        {
            var principal = serviceProvider.GetService<ClaimsPrincipal>();

            entities
            .Where(x => x.State == EntityState.Modified)
            .ToList()
            .ForEach(x =>
            {
                var entity = x.Entity;
                entity.ModifiedDate = DateTime.Now;
                entity.ModifiedByUserId = principal?.Identity?.Name ?? AnonymousUserId;
            });
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var operationalStoreOptions = new OperationalStoreOptions
            {
                ConfigureDbContext = x => x.UseSqlServer(_identityServerConfig.ConnectionString)
            };
            var configurationStoreOptions = new ConfigurationStoreOptions
            {
                ConfigureDbContext = x => x.UseSqlServer(_identityServerConfig.ConnectionString)
            };

            builder.ConfigurePersistedGrantContext(operationalStoreOptions);
            builder.ConfigureClientContext(configurationStoreOptions);
            builder.ConfigureResourcesContext(configurationStoreOptions);
            new DataSeeder().SeedData(builder);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var environment = serviceProvider.GetRequiredService<IHostEnvironment>();
            if (environment.IsDevelopment())
            {
                optionsBuilder.EnableSensitiveDataLogging();
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
