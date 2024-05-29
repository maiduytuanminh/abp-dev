using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Swashbuckle;

[DependsOn(
    typeof(SmartSoftwareVirtualFileSystemModule),
    typeof(SmartSoftwareAspNetCoreMvcModule))]
public class SmartSoftwareSwashbuckleModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareSwashbuckleModule>();
        });
    }
}
