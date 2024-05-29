using SmartSoftware.AspNetCore.Components.Server.Theming;
using SmartSoftware.Modularity;

namespace SmartSoftware.FeatureManagement.Blazor.Server;

[DependsOn(
    typeof(SmartSoftwareFeatureManagementBlazorModule),
    typeof(SmartSoftwareAspNetCoreComponentsServerThemingModule)
    )]
public class SmartSoftwareFeatureManagementBlazorServerModule : SmartSoftwareModule
{

}
