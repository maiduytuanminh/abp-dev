using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;

namespace SmartSoftware;

public static class SmartSoftwareApplicationFactory
{
    public async static Task<ISmartSoftwareApplicationWithInternalServiceProvider> CreateAsync<TStartupModule>(
        Action<SmartSoftwareApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : ISmartSoftwareModule
    {
        var app = Create(typeof(TStartupModule), options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    public async static Task<ISmartSoftwareApplicationWithInternalServiceProvider> CreateAsync(
        [NotNull] Type startupModuleType,
        Action<SmartSoftwareApplicationCreationOptions>? optionsAction = null)
    {
        var app = new SmartSoftwareApplicationWithInternalServiceProvider(startupModuleType, options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    public async static Task<ISmartSoftwareApplicationWithExternalServiceProvider> CreateAsync<TStartupModule>(
        [NotNull] IServiceCollection services,
        Action<SmartSoftwareApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : ISmartSoftwareModule
    {
        var app = Create(typeof(TStartupModule), services, options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    public async static Task<ISmartSoftwareApplicationWithExternalServiceProvider> CreateAsync(
        [NotNull] Type startupModuleType,
        [NotNull] IServiceCollection services,
        Action<SmartSoftwareApplicationCreationOptions>? optionsAction = null)
    {
        var app = new SmartSoftwareApplicationWithExternalServiceProvider(startupModuleType, services, options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    public static ISmartSoftwareApplicationWithInternalServiceProvider Create<TStartupModule>(
        Action<SmartSoftwareApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : ISmartSoftwareModule
    {
        return Create(typeof(TStartupModule), optionsAction);
    }

    public static ISmartSoftwareApplicationWithInternalServiceProvider Create(
        [NotNull] Type startupModuleType,
        Action<SmartSoftwareApplicationCreationOptions>? optionsAction = null)
    {
        return new SmartSoftwareApplicationWithInternalServiceProvider(startupModuleType, optionsAction);
    }

    public static ISmartSoftwareApplicationWithExternalServiceProvider Create<TStartupModule>(
        [NotNull] IServiceCollection services,
        Action<SmartSoftwareApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : ISmartSoftwareModule
    {
        return Create(typeof(TStartupModule), services, optionsAction);
    }

    public static ISmartSoftwareApplicationWithExternalServiceProvider Create(
        [NotNull] Type startupModuleType,
        [NotNull] IServiceCollection services,
        Action<SmartSoftwareApplicationCreationOptions>? optionsAction = null)
    {
        return new SmartSoftwareApplicationWithExternalServiceProvider(startupModuleType, services, optionsAction);
    }
}
