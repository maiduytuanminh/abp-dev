using Microsoft.AspNetCore.Mvc;
using SmartSoftware.GlobalFeatures;

namespace SmartSoftware.AspNetCore.Mvc.GlobalFeatures;

[RequiresGlobalFeature(SmartSoftwareAspNetCoreMvcTestFeature1.Name)]
[Route("api/EnabledGlobalFeatureTestController-Test")]
public class EnabledGlobalFeatureTestController : SmartSoftwareController
{
    [HttpGet]
    [Route("TestMethod")]
    public string TestMethod()
    {
        return "TestMethod";
    }
}
