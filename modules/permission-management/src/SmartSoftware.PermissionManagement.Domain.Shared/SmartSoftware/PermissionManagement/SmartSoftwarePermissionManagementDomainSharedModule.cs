using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.Localization;
using SmartSoftware.Validation;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.PermissionManagement;

[DependsOn(
    typeof(SmartSoftwareValidationModule)
    )]
public class SmartSoftwarePermissionManagementDomainSharedModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwarePermissionManagementDomainSharedModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<SmartSoftwarePermissionManagementResource>("en")
                .AddBaseTypes(
                    typeof(SmartSoftwareValidationResource)
                ).AddVirtualJson("/SmartSoftware/PermissionManagement/Localization/Domain");
        });
    }
}
