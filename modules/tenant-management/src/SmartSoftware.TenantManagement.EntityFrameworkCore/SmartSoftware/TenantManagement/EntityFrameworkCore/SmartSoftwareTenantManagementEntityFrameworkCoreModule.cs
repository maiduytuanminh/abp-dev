using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Modularity;

namespace SmartSoftware.TenantManagement.EntityFrameworkCore;

[DependsOn(typeof(SmartSoftwareTenantManagementDomainModule))]
[DependsOn(typeof(SmartSoftwareEntityFrameworkCoreModule))]
public class SmartSoftwareTenantManagementEntityFrameworkCoreModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSmartSoftwareDbContext<TenantManagementDbContext>(options =>
        {
            options.AddDefaultRepositories<ITenantManagementDbContext>();
        });
    }
}
