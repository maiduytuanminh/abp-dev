using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.Localization;
using SmartSoftware.Localization.ExceptionHandling;
using SmartSoftware.Modularity;
using SmartSoftware.Validation;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.CmsKit.Localization;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(SmartSoftwareValidationModule),
    typeof(SmartSoftwareGlobalFeaturesModule),
    typeof(SmartSoftwareFeaturesModule)
)]
public class CmsKitDomainSharedModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitDomainSharedModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<CmsKitResource>("en")
                .AddBaseTypes(typeof(SmartSoftwareValidationResource))
                .AddVirtualJson("SmartSoftware/CmsKit/Localization/Resources");
        });

        Configure<SmartSoftwareExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("CmsKit", typeof(CmsKitResource));
        });
    }
}
