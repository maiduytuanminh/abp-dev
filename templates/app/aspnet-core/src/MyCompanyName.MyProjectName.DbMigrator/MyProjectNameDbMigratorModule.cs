using MyCompanyName.MyProjectName.EntityFrameworkCore;
using SmartSoftware.Autofac;
//<TEMPLATE-REMOVE IF-NOT='TIERED'>
using SmartSoftware.Caching;
using SmartSoftware.Caching.StackExchangeRedis;
//</TEMPLATE-REMOVE>
using SmartSoftware.Modularity;

namespace MyCompanyName.MyProjectName.DbMigrator;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    //<TEMPLATE-REMOVE IF-NOT='TIERED'>
    typeof(SmartSoftwareCachingStackExchangeRedisModule),
    //</TEMPLATE-REMOVE>
    typeof(MyProjectNameEntityFrameworkCoreModule),
    typeof(MyProjectNameApplicationContractsModule)
    )]
public class MyProjectNameDbMigratorModule : SmartSoftwareModule
{
    //<TEMPLATE-REMOVE IF-NOT='TIERED'>
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDistributedCacheOptions>(options => { options.KeyPrefix = "MyProjectName:"; });
    }
    //</TEMPLATE-REMOVE>
}
