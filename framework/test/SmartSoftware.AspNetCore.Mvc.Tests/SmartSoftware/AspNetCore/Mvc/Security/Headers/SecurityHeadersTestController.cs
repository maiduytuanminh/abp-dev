using Microsoft.AspNetCore.Mvc;

namespace SmartSoftware.AspNetCore.Mvc.Security.Headers;

public class SecurityHeadersTestController : SmartSoftwareController
{
    public ActionResult Get()
    {
        return Content("OK");
    }
}
