using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Users;

[DependsOn(
    typeof(SmartSoftwareVirtualFileSystemModule)
    )]
public class SmartSoftwareUsersInstallerModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareUsersInstallerModule>();
        });
    }
}
