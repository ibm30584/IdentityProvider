using IdentityProvider.Infrastructures.Services.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;

namespace IdentityProvider.Infrastructures.Services.IdentityServer4;


public static class Extensions
{
    public static IServiceCollection AddIdentityServer4WithStores(this IServiceCollection services, X509Certificate2 certificate)
    {
        services.AddIdentityServer()
            .AddConfigurationStore<IdentityProviderDbContext>()
            .AddOperationalStore<IdentityProviderDbContext>()
            .AddSigningCredential(certificate);
        return services;
    }

    public static IServiceCollection AddIdentityServer4InMemory(this IServiceCollection services, X509Certificate2 certificate)
    {
        services.AddIdentityServer()
            .AddInMemoryIdentityResources(InMemoryConfigurations.IdentityResources)
            .AddInMemoryApiScopes(InMemoryConfigurations.Scopes)
            .AddInMemoryClients(InMemoryConfigurations.Clients)
            .AddSigningCredential(certificate);
        return services;
    }

    public static WebApplication UseIdentityServer4(this WebApplication app)
    {
        app.UseMiddleware<FormatMiddleware>();
        app.UseIdentityServer();
        return app;
    }
}
