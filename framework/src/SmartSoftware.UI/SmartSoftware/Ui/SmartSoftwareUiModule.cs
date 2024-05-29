using Localization.Resources.SmartSoftwareUi;
using SmartSoftware.ExceptionHandling;
using SmartSoftware.ExceptionHandling.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.UI;

[DependsOn(
    typeof(SmartSoftwareExceptionHandlingModule)
)]
public class SmartSoftwareUiModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareUiModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<SmartSoftwareUiResource>("en")
                .AddBaseTypes(typeof(SmartSoftwareExceptionHandlingResource))
                .AddVirtualJson("/Localization/Resources/SmartSoftwareUi");
        });
    }
}
