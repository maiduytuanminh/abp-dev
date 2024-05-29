using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.TestBase;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreTestBaseModule),
    typeof(SmartSoftwareAspNetCoreMvcUiBundlingModule),
    typeof(SmartSoftwareAutofacModule)
)]
public class SmartSoftwareAspNetCoreMvcUiTestModule : SmartSoftwareModule
{

}
