using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Account;

[DependsOn(
    typeof(SmartSoftwareVirtualFileSystemModule)
    )]
public class SmartSoftwareAccountInstallerModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAccountInstallerModule>();
        });
    }
}
