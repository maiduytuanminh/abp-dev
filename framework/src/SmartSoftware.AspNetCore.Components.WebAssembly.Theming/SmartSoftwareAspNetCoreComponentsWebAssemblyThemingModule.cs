using SmartSoftware.AspNetCore.Components.Web.Theming;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Components.WebAssembly.Theming;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreComponentsWebThemingModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebAssemblyModule)
)]
public class SmartSoftwareAspNetCoreComponentsWebAssemblyThemingModule : SmartSoftwareModule
{

}
