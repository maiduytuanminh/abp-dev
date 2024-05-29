using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.TextTemplating.Scriban;

[DependsOn(
    typeof(SmartSoftwareTextTemplatingTestModule)
)]
public class ScribanTextTemplatingTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ScribanTextTemplatingTestModule>("SmartSoftware.TextTemplating.Scriban");
        });
    }
}
