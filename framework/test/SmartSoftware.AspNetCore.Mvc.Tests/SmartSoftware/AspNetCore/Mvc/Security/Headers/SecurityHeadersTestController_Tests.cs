﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shouldly;
using SmartSoftware.AspNetCore.Security;
using Xunit;

namespace SmartSoftware.AspNetCore.Mvc.Security.Headers;

public class SecurityHeadersTestController_Tests : AspNetCoreMvcTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<SmartSoftwareSecurityHeadersOptions>(options =>
        {
            options.UseContentSecurityPolicyHeader = true;
            options.Headers["Referrer-Policy"] = "no-referrer";
        });

        base.ConfigureServices(services);
    }

    [Fact]
    public async Task SecurityHeaders_Should_Be_Added()
    {
        var responseMessage = await GetResponseAsync("/SecurityHeadersTest/Get");
        responseMessage.Headers.ShouldContain(x => x.Key == "X-Content-Type-Options" & x.Value.First().ToString() == "nosniff");
        responseMessage.Headers.ShouldContain(x => x.Key == "X-XSS-Protection" & x.Value.First().ToString() == "1; mode=block");
        responseMessage.Headers.ShouldContain(x => x.Key == "X-Frame-Options" & x.Value.First().ToString() == "SAMEORIGIN");
        responseMessage.Headers.ShouldContain(x => x.Key == "X-Content-Type-Options" & x.Value.First().ToString() == "nosniff");
    }

    [Fact]
    public async Task SecurityHeaders_Custom_Headers_Should_Be_Added()
    {
        var responseMessage = await GetResponseAsync("/SecurityHeadersTest/Get");
        responseMessage.Headers.ShouldNotBeEmpty();
        responseMessage.Headers.ShouldContain(x => x.Key == "Referrer-Policy" && x.Value.First().ToString() == "no-referrer");
    }
}
