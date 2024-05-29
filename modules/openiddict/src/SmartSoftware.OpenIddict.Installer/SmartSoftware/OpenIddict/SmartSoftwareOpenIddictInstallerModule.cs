using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.OpenIddict;

[DependsOn(
    typeof(SmartSoftwareVirtualFileSystemModule)
)]
public class SmartSoftwareOpenIddictInstallerModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareOpenIddictInstallerModule>();
        });
    }
}
