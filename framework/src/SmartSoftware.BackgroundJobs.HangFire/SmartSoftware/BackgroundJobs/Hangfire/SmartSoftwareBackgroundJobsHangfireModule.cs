using System;
using System.Linq;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Hangfire;
using SmartSoftware.Modularity;

namespace SmartSoftware.BackgroundJobs.Hangfire;

[DependsOn(
    typeof(SmartSoftwareBackgroundJobsAbstractionsModule),
    typeof(SmartSoftwareHangfireModule)
)]
public class SmartSoftwareBackgroundJobsHangfireModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(serviceProvider =>
            serviceProvider.GetRequiredService<SmartSoftwareDashboardOptionsProvider>().Get());
    }

    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareBackgroundJobOptions>>().Value;
        if (!options.IsJobExecutionEnabled)
        {
            var hangfireOptions = context.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareHangfireOptions>>().Value;
            hangfireOptions.BackgroundJobServerFactory = CreateOnlyEnqueueJobServer;
        }
    }

    private BackgroundJobServer? CreateOnlyEnqueueJobServer(IServiceProvider serviceProvider)
    {
        serviceProvider.GetRequiredService<JobStorage>();
        return null;
    }
}
