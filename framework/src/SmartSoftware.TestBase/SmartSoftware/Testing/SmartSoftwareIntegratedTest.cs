using System;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;

namespace SmartSoftware.Testing;

public abstract class SmartSoftwareIntegratedTest<TStartupModule> : SmartSoftwareTestBaseWithServiceProvider, IDisposable
    where TStartupModule : ISmartSoftwareModule
{
    protected ISmartSoftwareApplication Application { get; }

    protected IServiceProvider RootServiceProvider { get; }

    protected IServiceScope TestServiceScope { get; }

    protected SmartSoftwareIntegratedTest()
    {
        var services = CreateServiceCollection();

        BeforeAddApplication(services);

        var application = services.AddApplication<TStartupModule>(SetSmartSoftwareApplicationCreationOptions);
        Application = application;

        AfterAddApplication(services);

        RootServiceProvider = CreateServiceProvider(services);
        TestServiceScope = RootServiceProvider.CreateScope();

        application.Initialize(TestServiceScope.ServiceProvider);
        ServiceProvider = Application.ServiceProvider;
    }

    protected virtual IServiceCollection CreateServiceCollection()
    {
        return new ServiceCollection();
    }

    protected virtual void BeforeAddApplication(IServiceCollection services)
    {

    }

    protected virtual void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {

    }

    protected virtual void AfterAddApplication(IServiceCollection services)
    {

    }

    protected virtual IServiceProvider CreateServiceProvider(IServiceCollection services)
    {
        return services.BuildServiceProviderFromFactory();
    }

    public virtual void Dispose()
    {
        Application.Shutdown();
        TestServiceScope.Dispose();
        Application.Dispose();
    }
}
