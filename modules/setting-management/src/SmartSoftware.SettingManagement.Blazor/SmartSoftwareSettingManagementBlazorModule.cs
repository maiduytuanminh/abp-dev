using Localization.Resources.SmartSoftwareUi;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Components.Web.Theming;
using SmartSoftware.AspNetCore.Components.Web.Theming.Routing;
using SmartSoftware.AutoMapper;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.SettingManagement.Blazor.Menus;
using SmartSoftware.SettingManagement.Blazor.Settings;
using SmartSoftware.SettingManagement.Localization;
using SmartSoftware.UI.Navigation;

namespace SmartSoftware.SettingManagement.Blazor;

[DependsOn(
    typeof(SmartSoftwareAutoMapperModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebThemingModule),
    typeof(SmartSoftwareSettingManagementApplicationContractsModule)
)]
public class SmartSoftwareSettingManagementBlazorModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<SmartSoftwareSettingManagementBlazorModule>();

        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<SettingManagementBlazorAutoMapperProfile>(validate: true);
        });

        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new SettingManagementMenuContributor());
        });

        Configure<SmartSoftwareRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(SmartSoftwareSettingManagementBlazorModule).Assembly);
        });

        Configure<SettingManagementComponentOptions>(options =>
        {
            options.Contributors.Add(new EmailingPageContributor());
            options.Contributors.Add(new TimeZonePageContributor());
        });
        
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SmartSoftwareSettingManagementResource>()
                .AddBaseTypes(
                    typeof(SmartSoftwareUiResource)
                );
        });
    }
}
