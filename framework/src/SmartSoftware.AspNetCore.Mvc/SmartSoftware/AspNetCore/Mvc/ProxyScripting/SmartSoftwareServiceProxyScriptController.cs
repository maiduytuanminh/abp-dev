using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSoftware.Auditing;
using SmartSoftware.Http;
using SmartSoftware.Http.ProxyScripting;
using SmartSoftware.Minify.Scripts;

namespace SmartSoftware.AspNetCore.Mvc.ProxyScripting;

[Area("SmartSoftware")]
[Route("SmartSoftware/ServiceProxyScript")]
[DisableAuditing]
[RemoteService(false)]
[ApiExplorerSettings(IgnoreApi = true)]
public class SmartSoftwareServiceProxyScriptController : SmartSoftwareController
{
    protected readonly IProxyScriptManager ProxyScriptManager;
    protected readonly SmartSoftwareAspNetCoreMvcOptions Options;
    protected readonly IJavascriptMinifier JavascriptMinifier;

    public SmartSoftwareServiceProxyScriptController(IProxyScriptManager proxyScriptManager,
        IOptions<SmartSoftwareAspNetCoreMvcOptions> options,
        IJavascriptMinifier javascriptMinifier)
    {
        ProxyScriptManager = proxyScriptManager;
        Options = options.Value;
        JavascriptMinifier = javascriptMinifier;
    }

    [HttpGet]
    [Produces(MimeTypes.Application.Javascript, MimeTypes.Text.Plain)]
    public virtual ActionResult GetAll(ServiceProxyGenerationModel model)
    {
        model.Normalize();

        var script = ProxyScriptManager.GetScript(model.CreateOptions());

        return Content(
            Options.MinifyGeneratedScript == true
                ? JavascriptMinifier.Minify(script)
                : script,
            MimeTypes.Application.Javascript
        );
    }
}
