using SmartSoftware.Modularity;
using SmartSoftware.Localization;
using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.Domain;
using SmartSoftware.Localization.ExceptionHandling;
using SmartSoftware.Validation;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    typeof(SmartSoftwareValidationModule),
    typeof(SmartSoftwareDddDomainSharedModule)
)]
public class MyProjectNameDomainSharedModule : SmartSoftwareModule
{
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
        });

        Configure<SmartSoftwareExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("MyProjectName", typeof(MyProjectNameResource));
        });
    }
}
