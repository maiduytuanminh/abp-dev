using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.AspNetCore.TestBase;

namespace SmartSoftware.Identity.AspNetCore;

public abstract class SmartSoftwareIdentityAspNetCoreTestBase : SmartSoftwareAspNetCoreIntegratedTestBase<SmartSoftwareIdentityAspNetCoreTestStartup>
{
    protected virtual async Task<string> GetResponseAsStringAsync(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    {
        var response = await GetResponseAsync(url, expectedStatusCode);
        return await response.Content.ReadAsStringAsync();
    }

    protected virtual async Task<HttpResponseMessage> GetResponseAsync(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    {
        var response = await Client.GetAsync(url);
        response.StatusCode.ShouldBe(expectedStatusCode);
        return response;
    }
}
