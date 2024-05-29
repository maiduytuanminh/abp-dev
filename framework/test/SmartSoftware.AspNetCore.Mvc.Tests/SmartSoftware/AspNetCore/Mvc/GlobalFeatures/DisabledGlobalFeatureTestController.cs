using Microsoft.AspNetCore.Mvc;
using SmartSoftware.GlobalFeatures;

namespace SmartSoftware.AspNetCore.Mvc.GlobalFeatures;

[RequiresGlobalFeature("Not-Enabled-Feature")]
[Route("api/DisabledGlobalFeatureTestController-Test")]
public class DisabledGlobalFeatureTestController : SmartSoftwareController
{
    [HttpGet]
    [Route("TestMethod")]
    public string TestMethod()
    {
        return "TestMethod";
    }
}
