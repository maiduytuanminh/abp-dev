using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.MultiTenancy;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.MultiTenancy.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.AspNetCore.Mvc.UI.MultiTenancy;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule),
    typeof(SmartSoftwareAspNetCoreMultiTenancyModule)
    )]
public class SmartSoftwareAspNetCoreMvcUiMultiTenancyModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(SmartSoftwareUiMultiTenancyResource),
                typeof(SmartSoftwareAspNetCoreMvcUiMultiTenancyModule).Assembly
            );
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareAspNetCoreMvcUiMultiTenancyModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAspNetCoreMvcUiMultiTenancyModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<SmartSoftwareUiMultiTenancyResource>("en")
                .AddVirtualJson("/SmartSoftware/AspNetCore/Mvc/UI/MultiTenancy/Localization");
        });

        Configure<SmartSoftwareBundlingOptions>(options =>
        {
            options.ScriptBundles
                .Get(StandardBundles.Scripts.Global)
                .AddFiles(
                    "/Pages/SmartSoftware/MultiTenancy/tenant-switch.js"
                );
        });
    }
}
