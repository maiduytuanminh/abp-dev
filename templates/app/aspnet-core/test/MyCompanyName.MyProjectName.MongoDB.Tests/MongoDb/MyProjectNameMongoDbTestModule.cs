using System;
using SmartSoftware.Data;
using SmartSoftware.Modularity;

namespace MyCompanyName.MyProjectName.MongoDB;

[DependsOn(
    typeof(MyProjectNameApplicationTestModule),
    typeof(MyProjectNameMongoDbModule)
)]
public class MyProjectNameMongoDbTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MyProjectNameMongoDbFixture.GetRandomConnectionString();
        });
    }
}
