using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Modularity;
using SmartSoftware.Quartz;

namespace SmartSoftware.BackgroundJobs.Quartz;

[DependsOn(
    typeof(SmartSoftwareBackgroundJobsAbstractionsModule),
    typeof(SmartSoftwareQuartzModule)
)]
public class SmartSoftwareBackgroundJobsQuartzModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(typeof(QuartzJobExecutionAdapter<>));
    }

    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareBackgroundJobOptions>>().Value;
        if (!options.IsJobExecutionEnabled)
        {
            var quartzOptions = context.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareQuartzOptions>>().Value;
            quartzOptions.StartSchedulerFactory = scheduler => Task.CompletedTask;
        }
    }
}
