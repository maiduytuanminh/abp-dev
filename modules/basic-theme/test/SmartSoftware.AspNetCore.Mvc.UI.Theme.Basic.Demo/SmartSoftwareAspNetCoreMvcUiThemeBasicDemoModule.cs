using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic.Demo.Menus;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Demo;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;
using SmartSoftware.UI.Navigation;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic.Demo;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcUiBasicThemeModule),
    typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedDemoModule),
    typeof(SmartSoftwareAutofacModule)
    )]
public class SmartSoftwareAspNetCoreMvcUiThemeBasicDemoModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var env = context.Services.GetHostingEnvironment();

        if (env.IsDevelopment())
        {
            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<SmartSoftwareAspNetCoreMvcUiThemeSharedDemoModule>(Path.Combine(env.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework{0}src{0}SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Demo", Path.DirectorySeparatorChar)));
            });
        }

        Configure<SmartSoftwareBundlingOptions>(options =>
        {
            options.StyleBundles
                .Get(StandardBundles.Styles.Global)
                .AddFiles("/demo/styles/main.css");
        });

        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new BasicThemeDemoMenuContributor());
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseConfiguredEndpoints();
    }
}
