using SmartSoftware.AspNetCore.Components.WebAssembly.Theming;
using SmartSoftware.Modularity;

namespace SmartSoftware.PermissionManagement.Blazor.WebAssembly;

[DependsOn(
    typeof(SmartSoftwarePermissionManagementBlazorModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebAssemblyThemingModule),
    typeof(SmartSoftwarePermissionManagementHttpApiClientModule)
)]
public class SmartSoftwarePermissionManagementBlazorWebAssemblyModule : SmartSoftwareModule
{
}
