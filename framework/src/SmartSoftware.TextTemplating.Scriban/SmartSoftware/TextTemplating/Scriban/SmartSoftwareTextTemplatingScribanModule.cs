using SmartSoftware.Modularity;

namespace SmartSoftware.TextTemplating.Scriban;

[DependsOn(
    typeof(SmartSoftwareTextTemplatingCoreModule)
)]
public class SmartSoftwareTextTemplatingScribanModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareTextTemplatingOptions>(options =>
        {
            options.DefaultRenderingEngine = ScribanTemplateRenderingEngine.EngineName;
            options.RenderingEngines[ScribanTemplateRenderingEngine.EngineName] = typeof(ScribanTemplateRenderingEngine);
        });
    }
}
