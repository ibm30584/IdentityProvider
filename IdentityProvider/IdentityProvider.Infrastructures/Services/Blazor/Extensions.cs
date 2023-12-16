using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IdentityProvider.Infrastructures.Services.Blazor
{
    public static class Extensions
    {
        public static IServiceCollection AddBlazor(this IServiceCollection services)
        {
            services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            return services;
        }
        public static WebApplication UseBlazor<TApp>(this WebApplication app, Assembly webAsembly)
            where TApp : ComponentBase
        {
            app.MapRazorComponents<TApp>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(webAsembly);

            return app;
        }
    }
}
