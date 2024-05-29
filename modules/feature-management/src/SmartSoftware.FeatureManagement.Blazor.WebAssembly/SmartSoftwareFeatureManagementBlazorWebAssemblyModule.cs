using SmartSoftware.AspNetCore.Components.WebAssembly.Theming;
using SmartSoftware.Modularity;

namespace SmartSoftware.FeatureManagement.Blazor.WebAssembly;

[DependsOn(
    typeof(SmartSoftwareFeatureManagementBlazorModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebAssemblyThemingModule),
    typeof(SmartSoftwareFeatureManagementHttpApiClientModule)
)]
public class SmartSoftwareFeatureManagementBlazorWebAssemblyModule : SmartSoftwareModule
{
}
