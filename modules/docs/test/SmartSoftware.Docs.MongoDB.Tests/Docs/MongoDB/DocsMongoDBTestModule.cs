using System;
using SmartSoftware.Data;
using SmartSoftware.Modularity;
using SmartSoftware.Uow;

namespace SmartSoftware.Docs.MongoDB
{
    [DependsOn(
        typeof(DocsTestBaseModule),
        typeof(DocsMongoDbModule)
    )]
    public class DocsMongoDBTestModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<SmartSoftwareDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
            });
        }
    }
}
