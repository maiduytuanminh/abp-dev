using SmartSoftware.Data;
using SmartSoftware.ExceptionHandling.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.ExceptionHandling;

/* TODO: This package is introduced in a later time by gathering
 * classes from multiple packages.
 * So, didn't change the original namespaces of the types to not introduce breaking changes.
 * We will re-design this package in a later time, probably with v5.0.
 */
[DependsOn(
    typeof(SmartSoftwareLocalizationModule),
    typeof(SmartSoftwareDataModule)
    )]
public class SmartSoftwareExceptionHandlingModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareExceptionHandlingModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<SmartSoftwareExceptionHandlingResource>("en")
                .AddVirtualJson("/SmartSoftware/ExceptionHandling/Localization");
        });
    }
}
