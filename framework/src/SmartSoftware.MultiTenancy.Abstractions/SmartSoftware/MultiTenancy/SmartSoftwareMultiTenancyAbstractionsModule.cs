using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy.Localization;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.MultiTenancy;

[DependsOn(
    typeof(SmartSoftwareVirtualFileSystemModule),
    typeof(SmartSoftwareLocalizationModule)
)]
public class SmartSoftwareMultiTenancyAbstractionsModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareMultiTenancyAbstractionsModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<SmartSoftwareMultiTenancyResource>("en")
                .AddVirtualJson("/SmartSoftware/MultiTenancy/Localization");
        });
    }
}
