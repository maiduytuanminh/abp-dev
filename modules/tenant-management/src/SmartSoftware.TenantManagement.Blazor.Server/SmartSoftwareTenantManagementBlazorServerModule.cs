using SmartSoftware.AspNetCore.Components.Server.Theming;
using SmartSoftware.FeatureManagement.Blazor.Server;
using SmartSoftware.Modularity;

namespace SmartSoftware.TenantManagement.Blazor.Server;

[DependsOn(
    typeof(SmartSoftwareTenantManagementBlazorModule),
    typeof(SmartSoftwareFeatureManagementBlazorServerModule)
    )]
public class SmartSoftwareTenantManagementBlazorServerModule : SmartSoftwareModule
{

}
