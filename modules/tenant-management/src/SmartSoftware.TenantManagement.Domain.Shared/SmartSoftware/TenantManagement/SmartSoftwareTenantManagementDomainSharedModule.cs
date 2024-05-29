using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.TenantManagement.Localization;
using SmartSoftware.Localization.ExceptionHandling;
using SmartSoftware.Validation;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.TenantManagement;

[DependsOn(typeof(SmartSoftwareValidationModule))]
public class SmartSoftwareTenantManagementDomainSharedModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareTenantManagementDomainSharedModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<SmartSoftwareTenantManagementResource>("en")
                .AddBaseTypes(
                    typeof(SmartSoftwareValidationResource)
                ).AddVirtualJson("/SmartSoftware/TenantManagement/Localization/Resources");
        });

        Configure<SmartSoftwareExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("SmartSoftware.TenantManagement", typeof(SmartSoftwareTenantManagementResource));
        });
    }
}
