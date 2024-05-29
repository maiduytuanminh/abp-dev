using SmartSoftware.Dapr;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.Dapr;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcModule),
    typeof(SmartSoftwareDaprModule)
)]
public class SmartSoftwareAspNetCoreMvcDaprModule : SmartSoftwareModule
{

}
