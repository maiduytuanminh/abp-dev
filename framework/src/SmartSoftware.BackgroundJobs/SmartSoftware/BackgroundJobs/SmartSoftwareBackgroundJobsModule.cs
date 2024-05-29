using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.BackgroundWorkers;
using SmartSoftware.Data;
using SmartSoftware.DistributedLocking;
using SmartSoftware.Guids;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Threading;
using SmartSoftware.Timing;

namespace SmartSoftware.BackgroundJobs;

[DependsOn(
    typeof(SmartSoftwareBackgroundJobsAbstractionsModule),
    typeof(SmartSoftwareBackgroundWorkersModule),
    typeof(SmartSoftwareTimingModule),
    typeof(SmartSoftwareGuidsModule),
    typeof(SmartSoftwareDistributedLockingAbstractionsModule),
    typeof(SmartSoftwareMultiTenancyModule)
    )]
public class SmartSoftwareBackgroundJobsModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        if (context.Services.IsDataMigrationEnvironment())
        {
            Configure<SmartSoftwareBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });
        }
    }

    public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        if (context.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareBackgroundJobOptions>>().Value.IsJobExecutionEnabled)
        {
            await context.AddBackgroundWorkerAsync<IBackgroundJobWorker>();
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
    }
}
