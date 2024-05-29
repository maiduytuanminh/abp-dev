using Microsoft.AspNetCore.Mvc;

namespace SmartSoftware.AspNetCore.Mvc.Auditing;

[Route("integration-api/audit-test")]
[IntegrationService]
public class AuditIntegrationServiceTestController : SmartSoftwareController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}