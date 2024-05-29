using SmartSoftware.AspNetCore.TestBase;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Tests.SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareAspNetCoreTestBaseModule),
    typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule)
)]
public class SmartSoftwareAspNetCoreMvcUiThemeSharedTestModule : SmartSoftwareModule
{

}
