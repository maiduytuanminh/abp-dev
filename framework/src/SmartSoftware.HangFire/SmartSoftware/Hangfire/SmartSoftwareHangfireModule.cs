using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Authorization;
using SmartSoftware.Modularity;

namespace SmartSoftware.Hangfire;

[DependsOn(typeof(SmartSoftwareAuthorizationAbstractionsModule))]
public class SmartSoftwareHangfireModule : SmartSoftwareModule
{
    private SmartSoftwareHangfireBackgroundJobServer? _backgroundJobServer;

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var preActions = context.Services.GetPreConfigureActions<IGlobalConfiguration>();
        context.Services.AddHangfire(configuration =>
        {
            preActions.Configure(configuration);
        });

        context.Services.AddSingleton(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<SmartSoftwareHangfireOptions>>().Value;
            return new SmartSoftwareHangfireBackgroundJobServer(options.BackgroundJobServerFactory.Invoke(serviceProvider));
        });
    }
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        _backgroundJobServer = context.ServiceProvider.GetRequiredService<SmartSoftwareHangfireBackgroundJobServer>();
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        if (_backgroundJobServer == null)
        {
            return;
        }

        _backgroundJobServer.HangfireJobServer?.SendStop();
        _backgroundJobServer.HangfireJobServer?.Dispose();
    }
}
