using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.BasicTheme;

[DependsOn(
    typeof(SmartSoftwareVirtualFileSystemModule)
    )]
public class SmartSoftwareBasicThemeInstallerModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareBasicThemeInstallerModule>();
        });
    }
}
