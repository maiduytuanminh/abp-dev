using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap;

[DependsOn(typeof(SmartSoftwareAspNetCoreMvcUiModule))]
public class SmartSoftwareAspNetCoreMvcUiBootstrapModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAspNetCoreMvcUiBootstrapModule>("SmartSoftware.AspNetCore.Mvc.UI.Bootstrap");
        });
    }
}
