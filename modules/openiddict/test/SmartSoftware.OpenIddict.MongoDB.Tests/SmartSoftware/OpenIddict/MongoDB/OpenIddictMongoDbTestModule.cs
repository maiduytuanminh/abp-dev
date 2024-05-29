using System;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.Modularity;
using SmartSoftware.Identity.MongoDB;
using SmartSoftware.MongoDB;
using SmartSoftware.Uow;

namespace SmartSoftware.OpenIddict.MongoDB;

[DependsOn(
    typeof(OpenIddictTestBaseModule),
    typeof(SmartSoftwareIdentityMongoDbModule),
    typeof(SmartSoftwareOpenIddictMongoDbModule)
    )]
public class OpenIddictMongoDbTestModule : SmartSoftwareModule
{
    private static string _connectionString;

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        _connectionString = MongoDbFixture.GetRandomConnectionString();
        Configure<SmartSoftwareDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = _connectionString;
        });
    }

    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        Migrate(context);
    }

    private static void Migrate(ApplicationInitializationContext context)
    {
        var dbContexts = context.ServiceProvider.GetServices<ISmartSoftwareMongoDbContext>();

        foreach (var dbContext in dbContexts)
        {
            var mongoUrl = new MongoUrl(_connectionString);
            var databaseName = mongoUrl.DatabaseName;
            var client = new MongoClient(mongoUrl);

            if (databaseName.IsNullOrWhiteSpace())
            {
                databaseName = ConnectionStringNameAttribute.GetConnStringName(dbContext.GetType());
            }

            (dbContext as SmartSoftwareMongoDbContext)?.InitializeCollections(client.GetDatabase(databaseName));
        }
    }
}
