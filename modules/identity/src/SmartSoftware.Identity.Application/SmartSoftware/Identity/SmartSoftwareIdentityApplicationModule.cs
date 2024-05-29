using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AutoMapper;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement;

namespace SmartSoftware.Identity;

[DependsOn(
    typeof(SmartSoftwareIdentityDomainModule),
    typeof(SmartSoftwareIdentityApplicationContractsModule),
    typeof(SmartSoftwareAutoMapperModule),
    typeof(SmartSoftwarePermissionManagementApplicationModule)
    )]
public class SmartSoftwareIdentityApplicationModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<SmartSoftwareIdentityApplicationModule>();

        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<SmartSoftwareIdentityApplicationModuleAutoMapperProfile>(validate: true);
        });
    }
}
