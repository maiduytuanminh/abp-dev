using SmartSoftware.Domain;
using SmartSoftware.Modularity;

namespace SmartSoftware.BlobStoring.Database;

[DependsOn(
    typeof(SmartSoftwareDddDomainModule),
    typeof(SmartSoftwareBlobStoringModule),
    typeof(BlobStoringDatabaseDomainSharedModule)
    )]
public class BlobStoringDatabaseDomainModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                if (container.ProviderType == null)
                {
                    container.UseDatabase();
                }
            });
        });
    }
}
