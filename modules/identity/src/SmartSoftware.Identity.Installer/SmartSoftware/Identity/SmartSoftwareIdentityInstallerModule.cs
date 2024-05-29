using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Identity
{
    [DependsOn(
        typeof(SmartSoftwareVirtualFileSystemModule)
        )]
    public class SmartSoftwareIdentityInstallerModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<SmartSoftwareIdentityInstallerModule>();
            });
        }
    }
}
