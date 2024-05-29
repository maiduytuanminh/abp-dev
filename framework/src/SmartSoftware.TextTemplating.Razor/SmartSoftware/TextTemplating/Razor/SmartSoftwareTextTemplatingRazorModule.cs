using System;
using SmartSoftware.Modularity;

namespace SmartSoftware.TextTemplating.Razor;

[DependsOn(
    typeof(SmartSoftwareTextTemplatingCoreModule)
)]
public class SmartSoftwareTextTemplatingRazorModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareTextTemplatingOptions>(options =>
        {
            if (options.DefaultRenderingEngine.IsNullOrWhiteSpace())
            {
                options.DefaultRenderingEngine = RazorTemplateRenderingEngine.EngineName;
            }
            options.RenderingEngines[RazorTemplateRenderingEngine.EngineName] = typeof(RazorTemplateRenderingEngine);
        });
    }
}
