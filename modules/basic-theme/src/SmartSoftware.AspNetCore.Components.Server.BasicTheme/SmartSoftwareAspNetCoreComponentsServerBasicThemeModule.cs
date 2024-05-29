using SmartSoftware.AspNetCore.Components.Server.BasicTheme.Bundling;
using SmartSoftware.AspNetCore.Components.Server.Theming;
using SmartSoftware.AspNetCore.Components.Server.Theming.Bundling;
using SmartSoftware.AspNetCore.Components.Web.BasicTheme;
using SmartSoftware.AspNetCore.Components.Web.Theming.Routing;
using SmartSoftware.AspNetCore.Components.Web.Theming.Toolbars;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Components.Server.BasicTheme;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreComponentsWebBasicThemeModule),
    typeof(SmartSoftwareAspNetCoreComponentsServerThemingModule)
    )]
public class SmartSoftwareAspNetCoreComponentsServerBasicThemeModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareToolbarOptions>(options =>
        {
            options.Contributors.Add(new BasicThemeToolbarContributor());
        });

        Configure<SmartSoftwareBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(BlazorBasicThemeBundles.Styles.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(BlazorStandardBundles.Styles.Global)
                        .AddContributors(typeof(BlazorBasicThemeStyleContributor));
                });

            options
                .ScriptBundles
                .Add(BlazorBasicThemeBundles.Scripts.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(BlazorStandardBundles.Scripts.Global)
                        .AddContributors(typeof(BlazorBasicThemeScriptContributor));
                });
        });
    }
}
