using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Shouldly;
using SmartSoftware.AspNetCore.Mvc.Auditing;
using SmartSoftware.AspNetCore.Mvc.ExceptionHandling;
using SmartSoftware.AspNetCore.Mvc.Features;
using SmartSoftware.AspNetCore.Mvc.GlobalFeatures;
using SmartSoftware.AspNetCore.Mvc.Response;
using SmartSoftware.AspNetCore.Mvc.Uow;
using SmartSoftware.AspNetCore.Mvc.Validation;
using Xunit;

namespace SmartSoftware.AspNetCore.Mvc.Filters;

[Route("api/enabled-features-test")]
public class EnabledSmartSoftwareFeaturesController : SmartSoftwareController, IRemoteService
{
    [HttpGet]
    public Task<List<string>> GetAsync()
    {
        var filters = HttpContext.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>()
            .FilterDescriptors.Where(x => x.Filter is ServiceFilterAttribute)
            .Select(x => x.Filter.As<ServiceFilterAttribute>().ServiceType.FullName).ToList();

        return Task.FromResult(filters);
    }
}

[Route("api/disabled-features-test")]
[DisableSmartSoftwareFeatures]
public class DisabledSmartSoftwareFeaturesController : SmartSoftwareController, IRemoteService
{
    [HttpGet]
    public Task<List<string>> GetAsync()
    {
        var filters = HttpContext.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>()
            .FilterDescriptors.Where(x => x.Filter is ServiceFilterAttribute)
            .Select(x => x.Filter.As<ServiceFilterAttribute>().ServiceType.FullName).ToList();

        return Task.FromResult(filters);
    }
}

public class MvcDisableSmartSoftwareFeaturesAttribute_Tests : AspNetCoreMvcTestBase
{
    [Fact]
    public async Task Should_Disable_MVC_Filters()
    {
        var filters = await GetResponseAsObjectAsync<List<string>>("/api/enabled-features-test");
        filters.ShouldContain(typeof(GlobalFeatureActionFilter).FullName);
        filters.ShouldContain(typeof(SmartSoftwareAuditActionFilter).FullName);
        filters.ShouldContain(typeof(SmartSoftwareNoContentActionFilter).FullName);
        filters.ShouldContain(typeof(SmartSoftwareFeatureActionFilter).FullName);
        filters.ShouldContain(typeof(SmartSoftwareValidationActionFilter).FullName);
        filters.ShouldContain(typeof(SmartSoftwareUowActionFilter).FullName);
        filters.ShouldContain(typeof(SmartSoftwareExceptionFilter).FullName);

        filters = await GetResponseAsObjectAsync<List<string>>("/api/disabled-features-test");
        filters.ShouldNotContain(typeof(GlobalFeatureActionFilter).FullName);
        filters.ShouldNotContain(typeof(SmartSoftwareAuditActionFilter).FullName);
        filters.ShouldNotContain(typeof(SmartSoftwareNoContentActionFilter).FullName);
        filters.ShouldNotContain(typeof(SmartSoftwareFeatureActionFilter).FullName);
        filters.ShouldNotContain(typeof(SmartSoftwareValidationActionFilter).FullName);
        filters.ShouldNotContain(typeof(SmartSoftwareUowActionFilter).FullName);
        filters.ShouldNotContain(typeof(SmartSoftwareExceptionFilter).FullName);
    }

}
