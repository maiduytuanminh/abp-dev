using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Blogging;

[DependsOn(
    typeof(SmartSoftwareVirtualFileSystemModule)
    )]
public class BloggingInstallerModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BloggingInstallerModule>();
        });
    }
}
