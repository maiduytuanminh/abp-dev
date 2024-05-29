using SmartSoftware.AspNetCore.Components.Server.Theming;
using SmartSoftware.Modularity;

namespace MyCompanyName.MyProjectName.Blazor.Server;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreComponentsServerThemingModule),
    typeof(MyProjectNameBlazorModule)
    )]
public class MyProjectNameBlazorServerModule : SmartSoftwareModule
{

}
