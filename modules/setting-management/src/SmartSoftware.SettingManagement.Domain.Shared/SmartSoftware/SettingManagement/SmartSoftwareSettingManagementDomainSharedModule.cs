using SmartSoftware.Features;
using SmartSoftware.Modularity;
using SmartSoftware.Localization;
using SmartSoftware.SettingManagement.Localization;
using SmartSoftware.Validation;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.SettingManagement;

[DependsOn(typeof(SmartSoftwareLocalizationModule),
    typeof(SmartSoftwareValidationModule),
    typeof(SmartSoftwareFeaturesModule))]
public class SmartSoftwareSettingManagementDomainSharedModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareSettingManagementDomainSharedModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<SmartSoftwareSettingManagementResource>("en")
                .AddBaseTypes(
                    typeof(SmartSoftwareValidationResource)
                ).AddVirtualJson("/SmartSoftware/SettingManagement/Localization/Resources/SmartSoftwareSettingManagement");
        });
    }
}
