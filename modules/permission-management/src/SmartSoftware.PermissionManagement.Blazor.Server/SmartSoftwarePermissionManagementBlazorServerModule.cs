using SmartSoftware.AspNetCore.Components.Server.Theming;
using SmartSoftware.Modularity;

namespace SmartSoftware.PermissionManagement.Blazor.Server;

[DependsOn(
    typeof(SmartSoftwarePermissionManagementBlazorModule),
    typeof(SmartSoftwareAspNetCoreComponentsServerThemingModule)
)]
public class SmartSoftwarePermissionManagementBlazorServerModule : SmartSoftwareModule
{
}
