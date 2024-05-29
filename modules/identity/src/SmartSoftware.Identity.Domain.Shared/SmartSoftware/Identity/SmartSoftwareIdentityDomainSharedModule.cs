using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Features;
using SmartSoftware.Identity.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Localization.ExceptionHandling;
using SmartSoftware.Modularity;
using SmartSoftware.Users;
using SmartSoftware.Validation;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Identity;

[DependsOn(
    typeof(SmartSoftwareUsersDomainSharedModule),
    typeof(SmartSoftwareValidationModule),
    typeof(SmartSoftwareFeaturesModule)
    )]
public class SmartSoftwareIdentityDomainSharedModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareIdentityDomainSharedModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<IdentityResource>("en")
                .AddBaseTypes(
                    typeof(SmartSoftwareValidationResource)
                ).AddVirtualJson("/SmartSoftware/Identity/Localization");
        });

        Configure<SmartSoftwareExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("SmartSoftware.Identity", typeof(IdentityResource));
        });
    }
}
