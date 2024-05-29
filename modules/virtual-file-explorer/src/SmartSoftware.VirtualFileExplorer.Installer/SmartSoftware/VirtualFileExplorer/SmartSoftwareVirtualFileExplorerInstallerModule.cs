using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.VirtualFileExplorer;

[DependsOn(
    typeof(SmartSoftwareVirtualFileSystemModule)
    )]
public class SmartSoftwareVirtualFileExplorerInstallerModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareVirtualFileExplorerInstallerModule>();
        });
    }
}
