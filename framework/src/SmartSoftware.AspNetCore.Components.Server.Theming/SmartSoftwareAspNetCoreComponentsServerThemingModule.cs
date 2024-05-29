using SmartSoftware.AspNetCore.Components.Server.Theming.Bundling;
using SmartSoftware.AspNetCore.Components.Web.Theming;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Components.Server.Theming;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreComponentsServerModule),
    typeof(SmartSoftwareAspNetCoreMvcUiPackagesModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebThemingModule),
    typeof(SmartSoftwareAspNetCoreMvcUiBundlingModule)
    )]
public class SmartSoftwareAspNetCoreComponentsServerThemingModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(BlazorStandardBundles.Styles.Global, bundle =>
                {
                    bundle.AddContributors(typeof(BlazorGlobalStyleContributor));
                });

            options
                .ScriptBundles
                .Add(BlazorStandardBundles.Scripts.Global, bundle =>
                {
                    bundle.AddContributors(typeof(BlazorGlobalScriptContributor));
                });
        });
    }
}
