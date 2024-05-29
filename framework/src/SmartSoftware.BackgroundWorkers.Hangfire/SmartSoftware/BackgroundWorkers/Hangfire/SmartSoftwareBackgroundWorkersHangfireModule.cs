using System;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Hangfire;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.BackgroundWorkers.Hangfire;

[DependsOn(
    typeof(SmartSoftwareBackgroundWorkersModule),
    typeof(SmartSoftwareHangfireModule))]
public class SmartSoftwareBackgroundWorkersHangfireModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton(typeof(HangfirePeriodicBackgroundWorkerAdapter<>));
    }
    
    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareBackgroundWorkerOptions>>().Value;
        if (!options.IsEnabled)
        {
            var hangfireOptions = context.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareHangfireOptions>>().Value;
            hangfireOptions.BackgroundJobServerFactory = CreateOnlyEnqueueJobServer;
        }
        
        context.ServiceProvider
            .GetRequiredService<HangfireBackgroundWorkerManager>()
            .Initialize(); 
    }
    
    private BackgroundJobServer? CreateOnlyEnqueueJobServer(IServiceProvider serviceProvider)
    {
        serviceProvider.GetRequiredService<JobStorage>();
        return null;
    }
}
