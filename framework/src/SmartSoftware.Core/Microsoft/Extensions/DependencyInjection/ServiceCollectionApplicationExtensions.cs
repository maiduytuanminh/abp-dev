using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SmartSoftware;
using SmartSoftware.Modularity;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionApplicationExtensions
{
    public static ISmartSoftwareApplicationWithExternalServiceProvider AddApplication<TStartupModule>(
        [NotNull] this IServiceCollection services,
        Action<SmartSoftwareApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : ISmartSoftwareModule
    {
        return SmartSoftwareApplicationFactory.Create<TStartupModule>(services, optionsAction);
    }

    public static ISmartSoftwareApplicationWithExternalServiceProvider AddApplication(
        [NotNull] this IServiceCollection services,
        [NotNull] Type startupModuleType,
        Action<SmartSoftwareApplicationCreationOptions>? optionsAction = null)
    {
        return SmartSoftwareApplicationFactory.Create(startupModuleType, services, optionsAction);
    }

    public async static Task<ISmartSoftwareApplicationWithExternalServiceProvider> AddApplicationAsync<TStartupModule>(
        [NotNull] this IServiceCollection services,
        Action<SmartSoftwareApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : ISmartSoftwareModule
    {
        return await SmartSoftwareApplicationFactory.CreateAsync<TStartupModule>(services,  optionsAction);
    }

    public async static Task<ISmartSoftwareApplicationWithExternalServiceProvider> AddApplicationAsync(
        [NotNull] this IServiceCollection services,
        [NotNull] Type startupModuleType,
        Action<SmartSoftwareApplicationCreationOptions>? optionsAction = null)
    {
        return await SmartSoftwareApplicationFactory.CreateAsync(startupModuleType, services, optionsAction);
    }

    public static string? GetApplicationName(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IApplicationInfoAccessor>().ApplicationName;
    }

    [NotNull]
    public static string GetApplicationInstanceId(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IApplicationInfoAccessor>().InstanceId;
    }

    [NotNull]
    public static ISmartSoftwareHostEnvironment GetSmartSoftwareHostEnvironment(this IServiceCollection services)
    {
        return services.GetSingletonInstance<ISmartSoftwareHostEnvironment>();
    }
}
