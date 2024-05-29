using SmartSoftware.AspNetCore.Components.WebAssembly.Theming;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.Blazor.WebAssembly;

namespace SmartSoftware.Identity.Blazor.WebAssembly;

[DependsOn(
    typeof(SmartSoftwareIdentityBlazorModule),
    typeof(SmartSoftwarePermissionManagementBlazorWebAssemblyModule),
    typeof(SmartSoftwareIdentityHttpApiClientModule)
)]
public class SmartSoftwareIdentityBlazorWebAssemblyModule : SmartSoftwareModule
{
}
