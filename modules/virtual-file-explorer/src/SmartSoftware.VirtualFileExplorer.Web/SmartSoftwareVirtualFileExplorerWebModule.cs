using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders.Physical;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Prismjs;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.UI.Navigation;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileExplorer.Web.Bundling;
using SmartSoftware.VirtualFileExplorer.Web.Localization;
using SmartSoftware.VirtualFileExplorer.Web.Navigation;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.VirtualFileExplorer.Web;

[DependsOn(typeof(SmartSoftwareAspNetCoreMvcUiBootstrapModule))]
[DependsOn(typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule))]
public class SmartSoftwareVirtualFileExplorerWebModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareVirtualFileExplorerWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var virtualFileExplorerOptions = context.Services.ExecutePreConfiguredActions<SmartSoftwareVirtualFileExplorerOptions>();

        if (virtualFileExplorerOptions.IsEnabled)
        {
            Configure<SmartSoftwareNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new VirtualFileExplorerMenuContributor());
            });

            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<SmartSoftwareVirtualFileExplorerWebModule>("SmartSoftware.VirtualFileExplorer.Web");
            });

            Configure<SmartSoftwareLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<VirtualFileExplorerResource>("en")
                    .AddBaseTypes(typeof(SmartSoftwareValidationResource))
                    .AddVirtualJson("/Localization/Resources");
            });

            Configure<SmartSoftwareBundleContributorOptions>(options =>
            {
                options
                    .Extensions<PrismjsStyleBundleContributor>()
                    .Add<PrismjsStyleBundleContributorDocsExtension>();

                options
                    .Extensions<PrismjsScriptBundleContributor>()
                    .Add<PrismjsScriptBundleContributorDocsExtension>();
            });
        }
    }
}
