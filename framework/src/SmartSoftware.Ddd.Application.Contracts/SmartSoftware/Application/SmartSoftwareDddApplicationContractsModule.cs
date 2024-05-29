using SmartSoftware.Application.Localization.Resources.SmartSoftwareDdd;
using SmartSoftware.Auditing;
using SmartSoftware.Data;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Application;

[DependsOn(
    typeof(SmartSoftwareLocalizationModule),
    typeof(SmartSoftwareAuditingContractsModule),
    typeof(SmartSoftwareDataModule)
    )]
public class SmartSoftwareDddApplicationContractsModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareDddApplicationContractsModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<SmartSoftwareDddApplicationContractsResource>("en")
                .AddVirtualJson("/SmartSoftware/Application/Localization/Resources/SmartSoftwareDdd");
        });
    }
}
