using System;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Data;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB.TestApp.FifthContext;
using SmartSoftware.MongoDB.TestApp.SecondContext;
using SmartSoftware.MongoDB.TestApp.ThirdDbContext;
using SmartSoftware.MultiTenancy;
using SmartSoftware.TestApp;
using SmartSoftware.TestApp.Domain;
using SmartSoftware.TestApp.MongoDb;
using SmartSoftware.TestApp.MongoDB;

namespace SmartSoftware.MongoDB;

[DependsOn(
    typeof(TestAppModule),
    typeof(SmartSoftwareMongoDbTestSecondContextModule)
    )]
public class SmartSoftwareMongoDbTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });

        context.Services.AddMongoDbContext<TestAppMongoDbContext>(options =>
        {
            options.AddDefaultRepositories<ITestAppMongoDbContext>();
            options.AddRepository<City, CityRepository>();

            options.ReplaceDbContext<IThirdDbContext>();
        });

        context.Services.AddMongoDbContext<HostTestAppDbContext>(options =>
        {
            options.AddDefaultRepositories<IFifthDbContext>();
            options.ReplaceDbContext<IFifthDbContext>(MultiTenancySides.Host);
        });

        context.Services.AddMongoDbContext<TenantTestAppDbContext>(options =>
        {
            options.AddDefaultRepositories<IFifthDbContext>();
        });
    }
}
