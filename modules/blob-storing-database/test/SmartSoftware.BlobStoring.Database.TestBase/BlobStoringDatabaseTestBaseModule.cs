using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Authorization;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.BlobStoring.Database;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareAuthorizationModule),
    typeof(BlobStoringDatabaseDomainModule),
    typeof(SmartSoftwareBlobStoringTestModule)
    )]
public class BlobStoringDatabaseTestBaseModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAlwaysAllowAuthorization();

        Configure<SmartSoftwareBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureAll((containerName, containerConfiguration) =>
            {
                containerConfiguration.UseDatabase();
            });
        });
    }
}
