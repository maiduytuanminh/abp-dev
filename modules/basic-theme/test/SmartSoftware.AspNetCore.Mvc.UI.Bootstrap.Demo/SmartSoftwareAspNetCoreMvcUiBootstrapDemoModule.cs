using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Demo.Favicon;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.HighlightJs;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Prismjs;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic.Demo.Menus;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;
using SmartSoftware.Ui.LayoutHooks;
using SmartSoftware.UI.Navigation;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Demo;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcUiBasicThemeModule),
    typeof(SmartSoftwareAutofacModule)
    )]
public class SmartSoftwareAspNetCoreMvcUiBootstrapDemoModule : SmartSoftwareModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseStaticFiles();
        app.UseConfiguredEndpoints();
    }
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareBundlingOptions>(options =>
        {
            options.StyleBundles
                .Get(StandardBundles.Styles.Global)
                .AddFiles("/css/demo.css")
                .AddContributors(typeof(PrismjsStyleBundleContributor))
                .AddContributors(typeof(HighlightJsStyleContributor));
            
            options.ScriptBundles
                .Get(StandardBundles.Scripts.Global)
                .AddFiles("/js/demo.js")
                .AddContributors(typeof(PrismjsScriptBundleContributor))
                .AddContributors(typeof(HighlightJsScriptContributor));
        } );
        
        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new BootstrapDemoMenuContributor());
        });
        
        Configure<SmartSoftwareBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                StandardBundles.Styles.Global, 
                bundleConfiguration =>
                {
                    bundleConfiguration.AddFiles("/styles/my-global-styles.css");
                }
            );
        });

        Configure<SmartSoftwareLayoutHookOptions>(options =>
        {
            options.Add(LayoutHooks.Head.First, typeof(FaviconViewComponent));
        });
    }
}
