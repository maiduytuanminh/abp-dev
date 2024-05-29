using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSoftware;

internal class SmartSoftwareApplicationWithInternalServiceProvider : SmartSoftwareApplicationBase, ISmartSoftwareApplicationWithInternalServiceProvider
{
    public IServiceScope? ServiceScope { get; private set; }

    public SmartSoftwareApplicationWithInternalServiceProvider(
        [NotNull] Type startupModuleType,
        Action<SmartSoftwareApplicationCreationOptions>? optionsAction
        ) : this(
        startupModuleType,
        new ServiceCollection(),
        optionsAction)
    {

    }

    private SmartSoftwareApplicationWithInternalServiceProvider(
        [NotNull] Type startupModuleType,
        [NotNull] IServiceCollection services,
        Action<SmartSoftwareApplicationCreationOptions>? optionsAction
        ) : base(
            startupModuleType,
            services,
            optionsAction)
    {
        Services.AddSingleton<ISmartSoftwareApplicationWithInternalServiceProvider>(this);
    }

    public IServiceProvider CreateServiceProvider()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (ServiceProvider != null)
        {
            return ServiceProvider;
        }

        ServiceScope = Services.BuildServiceProviderFromFactory().CreateScope();
        SetServiceProvider(ServiceScope.ServiceProvider);

        return ServiceProvider!;
    }

    public async Task InitializeAsync()
    {
        CreateServiceProvider();
        await InitializeModulesAsync();
    }

    public void Initialize()
    {
        CreateServiceProvider();
        InitializeModules();
    }

    public override void Dispose()
    {
        base.Dispose();
        ServiceScope?.Dispose();
    }
}
