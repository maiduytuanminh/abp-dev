using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Docs;

[DependsOn(
    typeof(SmartSoftwareVirtualFileSystemModule)
    )]
public class DocsInstallerModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DocsInstallerModule>();
        });
    }
}
