using System;
using SmartSoftware.Data;
using SmartSoftware.Identity.MongoDB;
using SmartSoftware.IdentityServer.MongoDB;
using SmartSoftware.Modularity;
using SmartSoftware.Uow;

namespace SmartSoftware.IdentityServer;

[DependsOn(
    typeof(SmartSoftwareIdentityServerTestBaseModule),
    typeof(SmartSoftwareIdentityServerMongoDbModule),
    typeof(SmartSoftwareIdentityMongoDbModule)
)]
public class SmartSoftwareIdentityServerMongoDbTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });

    }
}
