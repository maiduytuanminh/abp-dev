using SmartSoftware.Autofac;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Sqlite;
using SmartSoftware.Modularity;

namespace SmartSoftware.Dapper;

[DependsOn(
    typeof(SmartSoftwareEntityFrameworkCoreTestModule),
    typeof(SmartSoftwareDapperModule),
    typeof(SmartSoftwareAutofacModule)
    )]
public class SmartSoftwareDapperTestModule : SmartSoftwareModule
{

}
