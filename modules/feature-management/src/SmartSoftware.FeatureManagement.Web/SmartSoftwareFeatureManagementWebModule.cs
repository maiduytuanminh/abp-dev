using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.FeatureManagement.Localization;
using SmartSoftware.FeatureManagement.Settings;
using SmartSoftware.Http.ProxyScripting.Generators.JQuery;
using SmartSoftware.Modularity;
using SmartSoftware.SettingManagement.Web;
using SmartSoftware.SettingManagement.Web.Pages.SettingManagement;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.FeatureManagement;

[DependsOn(
    typeof(SmartSoftwareFeatureManagementApplicationContractsModule),
    typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule),
    typeof(SmartSoftwareSettingManagementWebModule)
    )]
public class SmartSoftwareFeatureManagementWebModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(SmartSoftwareFeatureManagementResource), typeof(SmartSoftwareFeatureManagementWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareFeatureManagementWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareFeatureManagementWebModule>();
        });

        Configure<RazorPagesOptions>(options =>
        {
                //Configure authorization.
        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule(FeatureManagementRemoteServiceConsts.ModuleName);
        });
        
        Configure<SettingManagementPageOptions>(options =>
        {
            options.Contributors.Add(new FeatureSettingManagementPageContributor());
        });
    }
}
