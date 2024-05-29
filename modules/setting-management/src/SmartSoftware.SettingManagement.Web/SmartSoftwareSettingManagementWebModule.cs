using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.AutoMapper;
using SmartSoftware.Http.ProxyScripting.Generators.JQuery;
using SmartSoftware.Modularity;
using SmartSoftware.SettingManagement.Localization;
using SmartSoftware.SettingManagement.Web.Navigation;
using SmartSoftware.SettingManagement.Web.Pages.SettingManagement;
using SmartSoftware.SettingManagement.Web.Settings;
using SmartSoftware.UI.Navigation;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.SettingManagement.Web;

[DependsOn(
    typeof(SmartSoftwareSettingManagementApplicationContractsModule),
    typeof(SmartSoftwareAutoMapperModule),
    typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule),
    typeof(SmartSoftwareSettingManagementDomainSharedModule)
    )]
public class SmartSoftwareSettingManagementWebModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(SmartSoftwareSettingManagementResource), typeof(SmartSoftwareSettingManagementWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareSettingManagementWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new SettingManagementMainMenuContributor());
        });

        Configure<SettingManagementPageOptions>(options =>
        {
            options.Contributors.Add(new EmailingPageContributor());
            options.Contributors.Add(new TimeZonePageContributor());
        });

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareSettingManagementWebModule>();
        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule(SettingManagementRemoteServiceConsts.ModuleName);
        });

        context.Services.AddAutoMapperObjectMapper<SmartSoftwareSettingManagementWebModule>();
        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<SettingManagementWebAutoMapperProfile>(validate: true);
        });
    }
}
