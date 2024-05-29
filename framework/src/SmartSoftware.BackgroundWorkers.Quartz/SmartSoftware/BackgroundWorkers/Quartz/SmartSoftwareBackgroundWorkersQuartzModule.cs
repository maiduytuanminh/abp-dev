using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SmartSoftware.Modularity;
using SmartSoftware.Quartz;
using SmartSoftware.Threading;

namespace SmartSoftware.BackgroundWorkers.Quartz;

[DependsOn(
    typeof(SmartSoftwareBackgroundWorkersModule),
    typeof(SmartSoftwareQuartzModule)
)]
public class SmartSoftwareBackgroundWorkersQuartzModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new SmartSoftwareQuartzConventionalRegistrar());
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton(typeof(QuartzPeriodicBackgroundWorkerAdapter<>));
    }

    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareBackgroundWorkerOptions>>().Value;
        if (!options.IsEnabled)
        {
            var quartzOptions = context.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareQuartzOptions>>().Value;
            quartzOptions.StartSchedulerFactory = _ => Task.CompletedTask;
        }
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var quartzBackgroundWorkerOptions = context.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareBackgroundWorkerQuartzOptions>>().Value;
        if (quartzBackgroundWorkerOptions.IsAutoRegisterEnabled)
        {
            var backgroundWorkerManager = context.ServiceProvider.GetRequiredService<IBackgroundWorkerManager>();
            var works = context.ServiceProvider.GetServices<IQuartzBackgroundWorker>().Where(x => x.AutoRegister);

            foreach (var work in works)
            {
                await backgroundWorkerManager.AddAsync(work);
            }
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
    }
}
