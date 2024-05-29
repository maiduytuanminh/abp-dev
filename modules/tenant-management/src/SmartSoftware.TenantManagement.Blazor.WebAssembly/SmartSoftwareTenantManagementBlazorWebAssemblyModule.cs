using SmartSoftware.AspNetCore.Components.WebAssembly.Theming;
using SmartSoftware.FeatureManagement.Blazor.WebAssembly;
using SmartSoftware.Modularity;

namespace SmartSoftware.TenantManagement.Blazor.WebAssembly;

[DependsOn(
    typeof(SmartSoftwareTenantManagementBlazorModule),
    typeof(SmartSoftwareFeatureManagementBlazorWebAssemblyModule),
    typeof(SmartSoftwareTenantManagementHttpApiClientModule)
    )]
public class SmartSoftwareTenantManagementBlazorWebAssemblyModule : SmartSoftwareModule
{

}
