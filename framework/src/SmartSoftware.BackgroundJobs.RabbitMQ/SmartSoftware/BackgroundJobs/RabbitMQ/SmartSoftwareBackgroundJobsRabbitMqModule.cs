using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.RabbitMQ;
using SmartSoftware.Threading;

namespace SmartSoftware.BackgroundJobs.RabbitMQ;

[DependsOn(
    typeof(SmartSoftwareBackgroundJobsAbstractionsModule),
    typeof(SmartSoftwareRabbitMqModule),
    typeof(SmartSoftwareThreadingModule)
)]
public class SmartSoftwareBackgroundJobsRabbitMqModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton(typeof(IJobQueue<>), typeof(JobQueue<>));
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await StartJobQueueManagerAsync(context);
    }

    public async override Task OnApplicationShutdownAsync(ApplicationShutdownContext context)
    {
        await StopJobQueueManagerAsync(context);
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationShutdownAsync(context));
    }

    private async static Task StartJobQueueManagerAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider
            .GetRequiredService<IJobQueueManager>()
            .StartAsync();
    }

    private async static Task StopJobQueueManagerAsync(ApplicationShutdownContext context)
    {
        await context.ServiceProvider
            .GetRequiredService<IJobQueueManager>()
            .StopAsync();
    }
}
