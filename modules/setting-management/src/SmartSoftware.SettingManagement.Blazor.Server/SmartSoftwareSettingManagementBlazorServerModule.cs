using SmartSoftware.AspNetCore.Components.Server.Theming;
using SmartSoftware.Modularity;

namespace SmartSoftware.SettingManagement.Blazor.Server;

[DependsOn(
    typeof(SmartSoftwareSettingManagementBlazorModule),
    typeof(SmartSoftwareAspNetCoreComponentsServerThemingModule)
)]
public class SmartSoftwareSettingManagementBlazorServerModule : SmartSoftwareModule
{
}
