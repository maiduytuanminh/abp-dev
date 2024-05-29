using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shouldly;
using SmartSoftware.AspNetCore.Mvc.Localization;
using Xunit;

namespace SmartSoftware.AspNetCore.Mvc.Controllers;

public class ReplaceBuiltInController_Tests : AspNetCoreMvcTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<SmartSoftwareAspNetCoreMvcOptions>(options =>
        {
            options.ControllersToRemove.Add(typeof(SmartSoftwareLanguagesController));
        });
    }

    [Fact]
    public async Task Test()
    {
        var random = Guid.NewGuid().ToString("N");

        (await GetResponseAsObjectAsync<MyApplicationConfigurationDto>("api/ss/application-configuration?random=" + random)).Random.ShouldBe(random);
        (await GetResponseAsObjectAsync<MyApplicationLocalizationDto>("api/ss/application-localization?CultureName=en&random=" + random)).Random.ShouldBe(random);

        (await GetResponseAsync("SmartSoftware/Languages/Switch", HttpStatusCode.NotFound)).StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
