using JetBrains.Annotations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware;
using SmartSoftware.AspNetCore.Components.WebAssembly.WebApp;
using SmartSoftware.Http.Client.Authentication;

namespace Microsoft.Extensions.DependencyInjection;

public static class SmartSoftwareBlazorWebAppServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorWebAppServices([NotNull] this IServiceCollection services)
    {
        Check.NotNull(services, nameof(services));

        services.AddSingleton<AuthenticationStateProvider, RemoteAuthenticationStateProvider>();
        services.Replace(ServiceDescriptor.Transient<ISmartSoftwareAccessTokenProvider, CookieBasedWebAssemblySmartSoftwareAccessTokenProvider>());

        return services;
    }

    public static IServiceCollection AddBlazorWebAppTieredServices([NotNull] this IServiceCollection services)
    {
        Check.NotNull(services, nameof(services));

        services.AddScoped<AuthenticationStateProvider, RemoteAuthenticationStateProvider>();
        services.Replace(ServiceDescriptor.Singleton<ISmartSoftwareAccessTokenProvider, PersistentComponentStateSmartSoftwareAccessTokenProvider>());

        return services;
    }
}
