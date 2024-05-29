using SmartSoftware.Modularity;
using SmartSoftware.Localization;
using SmartSoftware.OpenIddict.Localization;
using SmartSoftware.Localization.ExceptionHandling;
using SmartSoftware.Validation;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.OpenIddict;

[DependsOn(
    typeof(SmartSoftwareValidationModule)
)]
public class SmartSoftwareOpenIddictDomainSharedModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareOpenIddictDomainSharedModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<SmartSoftwareOpenIddictResource>("en")
                .AddBaseTypes(typeof(SmartSoftwareValidationResource))
                .AddVirtualJson("SmartSoftware/OpenIddict/Localization/OpenIddict");
        });

        Configure<SmartSoftwareExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("OpenIddict", typeof(SmartSoftwareOpenIddictResource));
        });
    }
}
