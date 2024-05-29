using SmartSoftware.Localization;
using SmartSoftware.Localization.Resources.SmartSoftwareLocalization;
using SmartSoftware.Modularity;
using SmartSoftware.Settings;
using SmartSoftware.Timing.Localization.Resources.SmartSoftwareTiming;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Timing;

[DependsOn(
    typeof(SmartSoftwareLocalizationModule),
    typeof(SmartSoftwareSettingsModule)
    )]
public class SmartSoftwareTimingModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareTimingModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options
                .Resources
                .Add<SmartSoftwareTimingResource>("en")
                .AddVirtualJson("/SmartSoftware/Timing/Localization");
        });
    }
}
