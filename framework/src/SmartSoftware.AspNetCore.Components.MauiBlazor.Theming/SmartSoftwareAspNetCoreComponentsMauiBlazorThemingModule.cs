using SmartSoftware.AspNetCore.Components.Web.Theming;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Components.MauiBlazor.Theming;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreComponentsWebThemingModule),
    typeof(SmartSoftwareAspNetCoreComponentsMauiBlazorModule)
)]
public class SmartSoftwareAspNetCoreComponentsMauiBlazorThemingModule : SmartSoftwareModule
{

}
