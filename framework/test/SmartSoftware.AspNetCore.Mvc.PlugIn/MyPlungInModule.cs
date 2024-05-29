using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.PlugIn;

[DependsOn(typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule))]
public class MyPlungInModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            //Add plugin assembly
            mvcBuilder.PartManager.ApplicationParts.Add(new AssemblyPart(typeof(MyPlungInModule).Assembly));

            //Add CompiledRazorAssemblyPart if the PlugIn module contains razor views.
            mvcBuilder.PartManager.ApplicationParts.Add(new CompiledRazorAssemblyPart(typeof(MyPlungInModule).Assembly));
        });
    }
}
