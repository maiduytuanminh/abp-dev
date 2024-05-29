using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.AntiForgery;
using SmartSoftware.Auditing;

namespace SmartSoftware.Swashbuckle;

[Area("SmartSoftware")]
[Route("SmartSoftware/Swashbuckle/[action]")]
[DisableAuditing]
[RemoteService(false)]
[ApiExplorerSettings(IgnoreApi = true)]
public class SmartSoftwareSwashbuckleController : SmartSoftwareController
{
    protected readonly ISmartSoftwareAntiForgeryManager AntiForgeryManager;

    public SmartSoftwareSwashbuckleController(ISmartSoftwareAntiForgeryManager antiForgeryManager)
    {
        AntiForgeryManager = antiForgeryManager;
    }

    [HttpGet]
    public virtual void SetCsrfCookie()
    {
        AntiForgeryManager.SetCookie();
    }
}
