using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc.AntiForgery;
using SmartSoftware.Auditing;
using SmartSoftware.Http;
using SmartSoftware.Json;
using SmartSoftware.Minify.Scripts;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

[Area("SmartSoftware")]
[Route("SmartSoftware/ApplicationConfigurationScript")]
[DisableAuditing]
[RemoteService(false)]
[ApiExplorerSettings(IgnoreApi = true)]
public class SmartSoftwareApplicationConfigurationScriptController : SmartSoftwareController
{
    protected readonly SmartSoftwareApplicationConfigurationAppService ConfigurationAppService;
    protected readonly IJsonSerializer JsonSerializer;
    protected readonly SmartSoftwareAspNetCoreMvcOptions Options;
    protected readonly IJavascriptMinifier JavascriptMinifier;
    protected readonly ISmartSoftwareAntiForgeryManager AntiForgeryManager;

    public SmartSoftwareApplicationConfigurationScriptController(
        SmartSoftwareApplicationConfigurationAppService configurationAppService,
        IJsonSerializer jsonSerializer,
        IOptions<SmartSoftwareAspNetCoreMvcOptions> options,
        IJavascriptMinifier javascriptMinifier,
        ISmartSoftwareAntiForgeryManager antiForgeryManager)
    {
        ConfigurationAppService = configurationAppService;
        JsonSerializer = jsonSerializer;
        Options = options.Value;
        JavascriptMinifier = javascriptMinifier;
        AntiForgeryManager = antiForgeryManager;
    }

    [HttpGet]
    [Produces(MimeTypes.Application.Javascript, MimeTypes.Text.Plain)]
    public virtual async Task<ActionResult> Get()
    {
        var script = CreateSmartSoftwareExtendScript(
            await ConfigurationAppService.GetAsync(
                new ApplicationConfigurationRequestOptions {
                    IncludeLocalizationResources = false
                }
            )
        );

        AntiForgeryManager.SetCookie();

        return Content(
            Options.MinifyGeneratedScript == true
                ? JavascriptMinifier.Minify(script)
                : script,
            MimeTypes.Application.Javascript
        );
    }

    protected virtual string CreateSmartSoftwareExtendScript(ApplicationConfigurationDto config)
    {
        var script = new StringBuilder();

        script.AppendLine("(function(){");
        script.AppendLine();
        script.AppendLine($"$.extend(true, ss, {JsonSerializer.Serialize(config, indented: true)})");
        script.AppendLine();
        script.AppendLine("ss.event.trigger('ss.configurationInitialized');");
        script.AppendLine();
        script.Append("})();");

        return script.ToString();
    }
}
