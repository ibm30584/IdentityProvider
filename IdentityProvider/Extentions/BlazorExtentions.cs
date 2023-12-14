using IdentityProvider.Components;

namespace IdentityProvider.Extentions
{
    public static class BlazorExtentions
    {
        public static IServiceCollection AddBlazor(this IServiceCollection services)
        {
            services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            return services;
        }
        public static WebApplication UseBlazor(this WebApplication app)
        {
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(UI._Imports).Assembly);

            return app;
        }
    }
}
