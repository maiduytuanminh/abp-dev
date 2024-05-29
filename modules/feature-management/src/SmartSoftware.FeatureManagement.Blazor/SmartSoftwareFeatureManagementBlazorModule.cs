using Localization.Resources.SmartSoftwareUi;
using SmartSoftware.AspNetCore.Components.Web.Theming;
using SmartSoftware.FeatureManagement.Blazor.Settings;
using SmartSoftware.FeatureManagement.Localization;
using SmartSoftware.Features;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.SettingManagement.Blazor;

namespace SmartSoftware.FeatureManagement.Blazor;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreComponentsWebThemingModule),
    typeof(SmartSoftwareFeatureManagementApplicationContractsModule),
    typeof(SmartSoftwareFeaturesModule),
    typeof(SmartSoftwareSettingManagementBlazorModule)
)]
public class SmartSoftwareFeatureManagementBlazorModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SettingManagementComponentOptions>(options =>
        {
            options.Contributors.Add(new FeatureSettingManagementComponentContributor());
        });
        
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SmartSoftwareFeatureManagementResource>()
                .AddBaseTypes(typeof(SmartSoftwareUiResource));
        });
    }
}
