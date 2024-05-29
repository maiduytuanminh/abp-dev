using System.Threading.Tasks;
using SmartSoftware.Autofac;
using SmartSoftware.BackgroundJobs.DemoApp.Shared;
using SmartSoftware.BackgroundJobs.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.SqlServer;
using SmartSoftware.Modularity;

namespace SmartSoftware.BackgroundJobs.DemoApp;

[DependsOn(
    typeof(DemoAppSharedModule),
    typeof(SmartSoftwareBackgroundJobsEntityFrameworkCoreModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareEntityFrameworkCoreSqlServerModule)
    )]
public class DemoAppModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDbContextOptions>(options =>
        {
            options.Configure(opts =>
            {
                opts.UseSqlServer();
            });
        });

        Configure<SmartSoftwareBackgroundJobWorkerOptions>(options =>
        {
            //Configure for fast running
            options.JobPollPeriod = 1000;
            options.DefaultFirstWaitDuration = 1;
            options.DefaultWaitFactor = 1;
        });
    }

    public override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        //TODO: Configure console logging
        //context
        //    .ServiceProvider
        //    .GetRequiredService<ILoggerFactory>()
        //    .AddConsole(LogLevel.Debug);

        return Task.CompletedTask;
    }
}
