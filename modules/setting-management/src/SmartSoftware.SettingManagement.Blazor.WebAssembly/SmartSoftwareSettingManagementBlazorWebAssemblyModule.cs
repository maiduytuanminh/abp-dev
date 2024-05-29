using SmartSoftware.AspNetCore.Components.WebAssembly.Theming;
using SmartSoftware.Modularity;

namespace SmartSoftware.SettingManagement.Blazor.WebAssembly;

[DependsOn(
    typeof(SmartSoftwareSettingManagementBlazorModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebAssemblyThemingModule),
    typeof(SmartSoftwareSettingManagementHttpApiClientModule)
)]
public class SmartSoftwareSettingManagementBlazorWebAssemblyModule : SmartSoftwareModule
{
}
