using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;
using SmartSoftware.Settings;
using SmartSoftware.Threading;

namespace SmartSoftware.SettingManagement;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareSettingManagementDomainModule))]
public class SmartSoftwareSettingManagementTestBaseModule : SmartSoftwareModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        SeedTestData(context);
    }

    private static void SeedTestData(ApplicationInitializationContext context)
    {
        using (var scope = context.ServiceProvider.CreateScope())
        {
            AsyncHelper.RunSync(() => scope.ServiceProvider
                .GetRequiredService<SettingTestDataBuilder>()
                .BuildAsync());
        }
    }
}
