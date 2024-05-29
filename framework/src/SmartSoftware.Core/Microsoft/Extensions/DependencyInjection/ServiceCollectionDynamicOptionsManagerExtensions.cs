using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SmartSoftware.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionDynamicOptionsManagerExtensions
{
    public static IServiceCollection AddSmartSoftwareDynamicOptions<TOptions, TManager>(this IServiceCollection services)
        where TOptions : class
        where TManager : SmartSoftwareDynamicOptionsManager<TOptions>
    {
        services.Replace(ServiceDescriptor.Scoped(typeof(IOptions<TOptions>), typeof(TManager)));
        services.Replace(ServiceDescriptor.Scoped(typeof(IOptionsSnapshot<TOptions>), typeof(TManager)));

        return services;
    }
}
