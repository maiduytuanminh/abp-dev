using System;
using SmartSoftware.Data;
using SmartSoftware.Modularity;
using SmartSoftware.Uow;

namespace SmartSoftware.BlobStoring.Database.MongoDB;

[DependsOn(
    typeof(BlobStoringDatabaseTestBaseModule),
    typeof(BlobStoringDatabaseMongoDbModule)
)]
public class BlobStoringDatabaseMongoDbTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
