using SmartSoftware.AspNetCore.Components.Server.Theming;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.Blazor.Server;

namespace SmartSoftware.Identity.Blazor.Server;

[DependsOn(
    typeof(SmartSoftwareIdentityBlazorModule),
    typeof(SmartSoftwarePermissionManagementBlazorServerModule)
)]
public class SmartSoftwareIdentityBlazorServerModule : SmartSoftwareModule
{
}
