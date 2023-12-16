using IdentityProvider.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityProvider.Infrastructures.Services.Persistence
{
    public static class Extensions
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IIdentityProviderDbContext, IdentityProviderDbContext>(x => x.UseSqlServer(connectionString));
            return services;
        }
    }
}
