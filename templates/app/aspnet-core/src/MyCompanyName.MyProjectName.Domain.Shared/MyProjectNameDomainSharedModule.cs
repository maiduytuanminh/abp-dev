using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.AuditLogging;
using SmartSoftware.BackgroundJobs;
using SmartSoftware.FeatureManagement;
using SmartSoftware.Identity;
using SmartSoftware.Localization;
using SmartSoftware.Localization.ExceptionHandling;
using SmartSoftware.Modularity;
using SmartSoftware.OpenIddict;
using SmartSoftware.PermissionManagement;
using SmartSoftware.SettingManagement;
using SmartSoftware.TenantManagement;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    typeof(SmartSoftwareAuditLoggingDomainSharedModule),
    typeof(SmartSoftwareBackgroundJobsDomainSharedModule),
    typeof(SmartSoftwareFeatureManagementDomainSharedModule),
    typeof(SmartSoftwareIdentityDomainSharedModule),
    typeof(SmartSoftwareOpenIddictDomainSharedModule),
    typeof(SmartSoftwarePermissionManagementDomainSharedModule),
    typeof(SmartSoftwareSettingManagementDomainSharedModule),
    typeof(SmartSoftwareTenantManagementDomainSharedModule)    
    )]
public class MyProjectNameDomainSharedModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        MyProjectNameGlobalFeatureConfigurator.Configure();
        MyProjectNameModuleExtensionConfigurator.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MyProjectNameDomainSharedModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<MyProjectNameResource>("en")
                .AddBaseTypes(typeof(SmartSoftwareValidationResource))
                .AddVirtualJson("/Localization/MyProjectName");

            options.DefaultResourceType = typeof(MyProjectNameResource);
        });

        Configure<SmartSoftwareExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("MyProjectName", typeof(MyProjectNameResource));
        });
    }
}
