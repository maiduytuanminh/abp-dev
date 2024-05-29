using SmartSoftware.Modularity;
using SmartSoftware.Localization;
using SmartSoftware.BlobStoring.Database.Localization;
using SmartSoftware.Localization.ExceptionHandling;
using SmartSoftware.Validation;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.BlobStoring.Database;

[DependsOn(
    typeof(SmartSoftwareValidationModule)
)]
public class BlobStoringDatabaseDomainSharedModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BlobStoringDatabaseDomainSharedModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<BlobStoringDatabaseResource>("en")
                .AddBaseTypes(typeof(SmartSoftwareValidationResource))
                .AddVirtualJson("/SmartSoftware/BlobStoring/Database/Localization");
        });

        Configure<SmartSoftwareExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("BlobStoringDatabase", typeof(BlobStoringDatabaseResource));
        });
    }
}
