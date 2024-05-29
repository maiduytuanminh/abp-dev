using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.Caching.StackExchangeRedis;

[DependsOn(
    typeof(SmartSoftwareCachingStackExchangeRedisModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareAutofacModule)
    )]
public class SmartSoftwareCachingStackExchangeRedisTestModule : SmartSoftwareModule
{

}
