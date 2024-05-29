using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.MultiTenancy;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic.Toolbars;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using SmartSoftware.AspNetCore.Mvc.UI.Theming;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule),
    typeof(SmartSoftwareAspNetCoreMvcUiMultiTenancyModule)
    )]
public class SmartSoftwareAspNetCoreMvcUiBasicThemeModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareAspNetCoreMvcUiBasicThemeModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareThemingOptions>(options =>
        {
            options.Themes.Add<BasicTheme>();

            if (options.DefaultThemeName == null)
            {
                options.DefaultThemeName = BasicTheme.Name;
            }
        });

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAspNetCoreMvcUiBasicThemeModule>("SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic");
        });

        Configure<SmartSoftwareToolbarOptions>(options =>
        {
            options.Contributors.Add(new BasicThemeMainTopToolbarContributor());
        });

        Configure<SmartSoftwareBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(BasicThemeBundles.Styles.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(StandardBundles.Styles.Global)
                        .AddContributors(typeof(BasicThemeGlobalStyleContributor));
                });

            options
                .ScriptBundles
                .Add(BasicThemeBundles.Scripts.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(StandardBundles.Scripts.Global)
                        .AddContributors(typeof(BasicThemeGlobalScriptContributor));
                });
        });
    }
}
