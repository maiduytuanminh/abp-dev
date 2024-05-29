using Localization.Resources.SmartSoftwareUi;
using SmartSoftware.AspNetCore.Components.Web.Theming;
using SmartSoftware.AutoMapper;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.Localization;

namespace SmartSoftware.PermissionManagement.Blazor;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreComponentsWebThemingModule),
    typeof(SmartSoftwareAutoMapperModule),
    typeof(SmartSoftwarePermissionManagementApplicationContractsModule)
    )]
public class SmartSoftwarePermissionManagementBlazorModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SmartSoftwarePermissionManagementResource>()
                .AddBaseTypes(
                    typeof(SmartSoftwareUiResource)
                );
        });
    }
}
