using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Autofac;
using SmartSoftware.BackgroundJobs.DemoApp.Shared;
using SmartSoftware.Modularity;
using Microsoft.Extensions.Configuration;
using SmartSoftware.BackgroundJobs.Hangfire;

namespace SmartSoftware.BackgroundJobs.DemoApp.HangFire;

[DependsOn(
    typeof(DemoAppSharedModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareBackgroundJobsHangfireModule)
)]
public class DemoAppHangfireModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services.PreConfigure<IGlobalConfiguration>(hangfireConfiguration =>
        {
            hangfireConfiguration.UseSqlServerStorage(configuration.GetConnectionString("Default"));
        });
    }
}
