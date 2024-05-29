using SmartSoftware.AspNetCore.Components.WebAssembly.Theming;
using SmartSoftware.Modularity;

namespace MyCompanyName.MyProjectName.Blazor.WebAssembly;

[DependsOn(
    typeof(MyProjectNameBlazorModule),
    typeof(MyProjectNameHttpApiClientModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class MyProjectNameBlazorWebAssemblyModule : SmartSoftwareModule
{

}
