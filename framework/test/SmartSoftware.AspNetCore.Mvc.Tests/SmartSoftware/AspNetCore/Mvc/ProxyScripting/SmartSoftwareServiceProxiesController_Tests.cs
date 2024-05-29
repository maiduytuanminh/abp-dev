using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace SmartSoftware.AspNetCore.Mvc.ProxyScripting;

public class SmartSoftwareServiceProxiesController_Tests : AspNetCoreMvcTestBase
{
    [Fact]
    public async Task GetAll()
    {
        var script = await GetResponseAsStringAsync("/SmartSoftware/ServiceProxyScript?minify=true");
        script.Length.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task GetAllWithMinify()
    {
        GetRequiredService<IOptions<SmartSoftwareAspNetCoreMvcOptions>>().Value.MinifyGeneratedScript = false;
        var script = await GetResponseAsStringAsync("/SmartSoftware/ServiceProxyScript");

        GetRequiredService<IOptions<SmartSoftwareAspNetCoreMvcOptions>>().Value.MinifyGeneratedScript = true;
        var minifyScript = await GetResponseAsStringAsync("/SmartSoftware/ServiceProxyScript?minify=true");

        script.Length.ShouldBeGreaterThan(0);
        minifyScript.Length.ShouldBeGreaterThan(0);
        minifyScript.Length.ShouldBeLessThan(script.Length);
    }
}
