using SmartSoftware.Http.ProxyScripting.Configuration;
using SmartSoftware.Http.ProxyScripting.Generators.JQuery;
using SmartSoftware.Json;
using SmartSoftware.Minify;
using SmartSoftware.Modularity;

namespace SmartSoftware.Http;

[DependsOn(typeof(SmartSoftwareHttpAbstractionsModule))]
[DependsOn(typeof(SmartSoftwareJsonModule))]
[DependsOn(typeof(SmartSoftwareMinifyModule))]
public class SmartSoftwareHttpModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareApiProxyScriptingOptions>(options =>
        {
            options.Generators[JQueryProxyScriptGenerator.Name] = typeof(JQueryProxyScriptGenerator);
        });
    }
}
