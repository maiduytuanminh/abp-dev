using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Tracing;

namespace SmartSoftware.AspNetCore.CorrelationIdProvider;

[Route("api/correlation")]
public class CorrelationIdProviderController : SmartSoftwareController
{
    [HttpGet]
    [Route("get")]
    public string Get()
    {
        return this.HttpContext.RequestServices.GetRequiredService<ICorrelationIdProvider>().Get();
    }
}
