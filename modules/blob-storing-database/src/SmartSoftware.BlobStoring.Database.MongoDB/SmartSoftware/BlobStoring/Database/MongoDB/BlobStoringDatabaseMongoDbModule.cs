using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;

namespace SmartSoftware.BlobStoring.Database.MongoDB;

[DependsOn(
    typeof(BlobStoringDatabaseDomainModule),
    typeof(SmartSoftwareMongoDbModule)
    )]
public class BlobStoringDatabaseMongoDbModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<BlobStoringMongoDbContext>(options =>
        {
            options.AddRepository<DatabaseBlobContainer, MongoDbDatabaseBlobContainerRepository>();
            options.AddRepository<DatabaseBlob, MongoDbDatabaseBlobRepository>();
        });
    }
}
