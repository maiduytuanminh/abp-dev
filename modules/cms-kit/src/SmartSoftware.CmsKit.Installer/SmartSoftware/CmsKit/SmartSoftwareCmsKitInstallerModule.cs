using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(SmartSoftwareVirtualFileSystemModule)
    )]
public class SmartSoftwareCmsKitInstallerModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareCmsKitInstallerModule>();
        });
    }
}
