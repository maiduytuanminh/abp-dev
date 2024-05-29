using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Application;
using SmartSoftware.AutoMapper;
using SmartSoftware.Modularity;

namespace SmartSoftware.TenantManagement;

[DependsOn(typeof(SmartSoftwareTenantManagementDomainModule))]
[DependsOn(typeof(SmartSoftwareTenantManagementApplicationContractsModule))]
[DependsOn(typeof(SmartSoftwareDddApplicationModule))]
public class SmartSoftwareTenantManagementApplicationModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<SmartSoftwareTenantManagementApplicationModule>();
        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<SmartSoftwareTenantManagementApplicationAutoMapperProfile>(validate: true);
        });
    }
}
