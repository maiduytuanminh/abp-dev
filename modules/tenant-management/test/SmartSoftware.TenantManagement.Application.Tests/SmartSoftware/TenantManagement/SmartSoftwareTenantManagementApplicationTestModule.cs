using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.TenantManagement.EntityFrameworkCore;

namespace SmartSoftware.TenantManagement;

[DependsOn(
    typeof(SmartSoftwareTenantManagementApplicationModule),
    typeof(SmartSoftwareTenantManagementEntityFrameworkCoreTestModule))]
public class SmartSoftwareTenantManagementApplicationTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAlwaysAllowAuthorization();
    }
}
