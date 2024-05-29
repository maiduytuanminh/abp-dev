using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Demo;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule)
    )]
public class SmartSoftwareAspNetCoreMvcUiThemeSharedDemoModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAspNetCoreMvcUiThemeSharedDemoModule>();
        });
    }
}
