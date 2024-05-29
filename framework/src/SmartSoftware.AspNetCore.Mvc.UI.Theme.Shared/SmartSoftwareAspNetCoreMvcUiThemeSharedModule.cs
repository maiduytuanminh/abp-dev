using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Widgets;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcUiBootstrapModule),
    typeof(SmartSoftwareAspNetCoreMvcUiPackagesModule),
    typeof(SmartSoftwareAspNetCoreMvcUiWidgetsModule)
    )]
public class SmartSoftwareAspNetCoreMvcUiThemeSharedModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAspNetCoreMvcUiThemeSharedModule>("SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared");
        });
        
        Configure<SmartSoftwareBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(StandardBundles.Styles.Global, bundle => { bundle.AddContributors(typeof(SharedThemeGlobalStyleContributor)); });

            options
                .ScriptBundles
                .Add(StandardBundles.Scripts.Global, bundle => bundle.AddContributors(typeof(SharedThemeGlobalScriptContributor)));
        });
    }
}
