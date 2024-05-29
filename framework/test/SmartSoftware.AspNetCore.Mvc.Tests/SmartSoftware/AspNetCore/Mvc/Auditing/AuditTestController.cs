using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSoftware.Auditing;

namespace SmartSoftware.AspNetCore.Mvc.Auditing;

[Route("api/audit-test")]
public class AuditTestController : SmartSoftwareController
{
    private readonly SmartSoftwareAuditingOptions _options;

    public AuditTestController(IOptions<SmartSoftwareAuditingOptions> options)
    {
        _options = options.Value;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [Route("audit-success")]
    public IActionResult AuditSuccessForGetRequests()
    {
        return Ok();
    }

    [Route("audit-fail")]
    public IActionResult AuditFailForGetRequests()
    {
        throw new UserFriendlyException("Exception occurred!");
    }

    [Route("audit-fail-object")]
    public object AuditFailForGetRequestsReturningObject()
    {
        throw new UserFriendlyException("Exception occurred!");
    }

    [HttpGet]
    [Route("audit-activate-failed")]
    public IActionResult AuditActivateFailed([FromServices] SmartSoftwareAuditingOptions options)
    {
        return Ok();
    }
}
