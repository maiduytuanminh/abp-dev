using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap;
using SmartSoftware.Minify;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcUiBootstrapModule),
    typeof(SmartSoftwareMinifyModule),
    typeof(SmartSoftwareAspNetCoreMvcUiBundlingAbstractionsModule)
    )]
public class SmartSoftwareAspNetCoreMvcUiBundlingModule : SmartSoftwareModule
{

}
