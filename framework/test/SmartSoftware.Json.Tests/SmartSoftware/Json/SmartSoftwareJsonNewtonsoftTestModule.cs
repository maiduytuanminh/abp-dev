using SmartSoftware.Autofac;
using SmartSoftware.Json.Newtonsoft;
using SmartSoftware.Json.SystemTextJson;
using SmartSoftware.Modularity;

namespace SmartSoftware.Json;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareJsonSystemTextJsonModule),
    typeof(SmartSoftwareTestBaseModule)
)]
public class SmartSoftwareJsonSystemTextJsonTestModule : SmartSoftwareModule
{

}

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareJsonNewtonsoftModule),
    typeof(SmartSoftwareTestBaseModule)
)]
public class SmartSoftwareJsonNewtonsoftTestModule : SmartSoftwareModule
{

}
