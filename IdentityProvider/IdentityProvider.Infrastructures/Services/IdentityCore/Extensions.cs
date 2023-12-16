using IdentityProvider.Domain.Entities;
using IdentityProvider.Infrastructures.Services.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityProvider.Infrastructures.Services.IdentityCore;


public static class IdentityServerExtensions
{
    public static IServiceCollection AddIdentityCore(this IServiceCollection services)
    {
        services
            .AddIdentityCore<AppUser>()
            .AddEntityFrameworkStores<IdentityProviderDbContext>()
            .AddApiEndpoints();

        services
            .AddAuthentication()
            .AddIdentityCookies();

        return services;
    }


    public static WebApplication UseIdentityCore(this WebApplication app)
    {
        app.MapIdentityApi<AppUser>();
        return app;
    }
}
