using SmartSoftware.Autofac;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.TestBase;

[DependsOn(typeof(SmartSoftwareHttpClientModule))]
[DependsOn(typeof(SmartSoftwareAspNetCoreModule))]
[DependsOn(typeof(SmartSoftwareTestBaseModule))]
[DependsOn(typeof(SmartSoftwareAutofacModule))]
public class SmartSoftwareAspNetCoreTestBaseModule : SmartSoftwareModule
{

}
