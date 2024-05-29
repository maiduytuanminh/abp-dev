using SmartSoftware.Domain;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Modularity;

namespace SmartSoftware.Dapper;

[DependsOn(
    typeof(SmartSoftwareDddDomainModule),
    typeof(SmartSoftwareEntityFrameworkCoreModule))]
public class SmartSoftwareDapperModule : SmartSoftwareModule
{
}
