using Microsoft.Extensions.DependencyInjection;
using SmartSoftware;
using SmartSoftware.Authorization;
using SmartSoftware.Autofac;
using SmartSoftware.BackgroundJobs;
using SmartSoftware.Data;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareAuthorizationModule),
    typeof(SmartSoftwareBackgroundJobsAbstractionsModule)
    )]
public class MyProjectNameTestBaseModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareBackgroundJobOptions>(options =>
        {
            options.IsJobExecutionEnabled = false;
        });

        context.Services.AddAlwaysAllowAuthorization();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        SeedTestData(context);
    }

    private static void SeedTestData(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(async () =>
        {
            using (var scope = context.ServiceProvider.CreateScope())
            {
                await scope.ServiceProvider
                    .GetRequiredService<IDataSeeder>()
                    .SeedAsync();
            }
        });
    }
}
