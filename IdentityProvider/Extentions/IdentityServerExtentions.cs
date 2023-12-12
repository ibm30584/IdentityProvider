using IdentityProvider.Configuration;

namespace IdentityProvider.Extentions;

public static class IdentityServerExtentions
{
    public static IServiceCollection AddIdentityServerServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var identityServerConfig = configuration
            .GetSection("IdentityServerConfig")
            .Get<IdentityServerConfig>();
        ArgumentNullException.ThrowIfNull(identityServerConfig);

        services.AddIdentityServer()
            .AddInMemoryIdentityResources(Configurations.IdentityResources)
            .AddInMemoryApiScopes(Configurations.Scopes)
            .AddInMemoryClients(Configurations.Clients)
            .AddSigningCredential(identityServerConfig!.GetSigningCertificate());
        return services;
    }

    public static WebApplication UseIdentityServerMiddlewares(this WebApplication app)
    {
        app.UseIdentityServiceResponseFormatter();
        app.UseIdentityServer();
        return app;
    }
}
