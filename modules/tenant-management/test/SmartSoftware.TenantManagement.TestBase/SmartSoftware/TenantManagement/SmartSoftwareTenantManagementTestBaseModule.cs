using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.TenantManagement;

[DependsOn(
    typeof(SmartSoftwareTenantManagementDomainModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareTestBaseModule)
    )]
public class SmartSoftwareTenantManagementTestBaseModule : SmartSoftwareModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        SeedTestData(context);
    }

    private static void SeedTestData(ApplicationInitializationContext context)
    {
        using (var scope = context.ServiceProvider.CreateScope())
        {
            scope.ServiceProvider
                .GetRequiredService<SmartSoftwareTenantManagementTestDataBuilder>()
                .Build();
        }
    }
}
