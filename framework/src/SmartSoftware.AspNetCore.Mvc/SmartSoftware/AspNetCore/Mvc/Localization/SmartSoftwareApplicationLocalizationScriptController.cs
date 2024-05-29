using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;
using SmartSoftware.Auditing;
using SmartSoftware.Http;
using SmartSoftware.Json;
using SmartSoftware.Localization;
using SmartSoftware.Minify.Scripts;

namespace SmartSoftware.AspNetCore.Mvc.Localization;

[Area("SmartSoftware")]
[Route("SmartSoftware/ApplicationLocalizationScript")]
[DisableAuditing]
[RemoteService(false)]
[ApiExplorerSettings(IgnoreApi = true)]
public class SmartSoftwareApplicationLocalizationScriptController : SmartSoftwareController
{
    protected SmartSoftwareApplicationLocalizationAppService LocalizationAppService { get; }
    protected SmartSoftwareAspNetCoreMvcOptions Options { get; }
    protected IJsonSerializer JsonSerializer { get; }
    protected IJavascriptMinifier JavascriptMinifier { get; }

    public SmartSoftwareApplicationLocalizationScriptController(
        SmartSoftwareApplicationLocalizationAppService localizationAppService,
        IOptions<SmartSoftwareAspNetCoreMvcOptions> options,
        IJsonSerializer jsonSerializer,
        IJavascriptMinifier javascriptMinifier)
    {
        LocalizationAppService = localizationAppService;
        JsonSerializer = jsonSerializer;
        JavascriptMinifier = javascriptMinifier;
        Options = options.Value;
    }

    [HttpGet]
    [Produces(MimeTypes.Application.Javascript, MimeTypes.Text.Plain)]
    public virtual async Task<ActionResult> GetAsync(ApplicationLocalizationRequestDto input)
    {
        var script = CreateScript(
            await LocalizationAppService.GetAsync(input)
        );

        return Content(
            Options.MinifyGeneratedScript == true
                ? JavascriptMinifier.Minify(script)
                : script,
            MimeTypes.Application.Javascript
        );
    }

    protected virtual string CreateScript(ApplicationLocalizationDto localizationDto)
    {
        var script = new StringBuilder();

        script.AppendLine("(function(){");
        script.AppendLine();
        script.AppendLine(
            $"$.extend(true, ss.localization, {JsonSerializer.Serialize(localizationDto, indented: true)})");
        script.AppendLine();
        script.Append("})();");

        return script.ToString();
    }
}
