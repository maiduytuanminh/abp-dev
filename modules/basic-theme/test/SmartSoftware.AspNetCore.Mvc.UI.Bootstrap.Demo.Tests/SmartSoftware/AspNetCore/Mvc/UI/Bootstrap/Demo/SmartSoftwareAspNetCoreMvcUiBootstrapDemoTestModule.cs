using SmartSoftware.AspNetCore.TestBase;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Demo;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcUiBootstrapDemoModule),
    typeof(SmartSoftwareAspNetCoreTestBaseModule)
)]
public class SmartSoftwareAspNetCoreMvcUiBootstrapDemoTestModule : SmartSoftwareModule
{

}
