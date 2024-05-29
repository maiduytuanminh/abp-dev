using SmartSoftware.AspNetCore.Components.WebAssembly;
using SmartSoftware.Modularity;

namespace SmartSoftware.Autofac.WebAssembly;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebAssemblyModule)
    )]
public class SmartSoftwareAutofacWebAssemblyModule : SmartSoftwareModule
{

}
