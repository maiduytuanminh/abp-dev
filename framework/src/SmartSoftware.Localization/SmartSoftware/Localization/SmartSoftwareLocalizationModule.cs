using SmartSoftware.Localization.Resources.SmartSoftwareLocalization;
using SmartSoftware.Modularity;
using SmartSoftware.Settings;
using SmartSoftware.Threading;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Localization;

[DependsOn(
    typeof(SmartSoftwareVirtualFileSystemModule),
    typeof(SmartSoftwareSettingsModule),
    typeof(SmartSoftwareLocalizationAbstractionsModule),
    typeof(SmartSoftwareThreadingModule)
    )]
public class SmartSoftwareLocalizationModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        SmartSoftwareStringLocalizerFactory.Replace(context.Services);

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareLocalizationModule>("SmartSoftware", "SmartSoftware");
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options
                .Resources
                .Add<DefaultResource>("en");

            options
                .Resources
                .Add<SmartSoftwareLocalizationResource>("en")
                .AddVirtualJson("/Localization/Resources/SmartSoftwareLocalization");
        });
    }
}
