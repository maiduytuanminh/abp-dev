using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Tracing;

namespace SmartSoftware.AspNetCore.App;

public class SerilogTestController : SmartSoftwareController
{
    private readonly ICorrelationIdProvider _correlationIdProvider;

    public SerilogTestController(ICorrelationIdProvider correlationIdProvider)
    {
        _correlationIdProvider = correlationIdProvider;
    }

    public ActionResult Index()
    {
        return Content("Index-Result");
    }

    public ActionResult CorrelationId()
    {
        return Content(_correlationIdProvider.Get());
    }
}
