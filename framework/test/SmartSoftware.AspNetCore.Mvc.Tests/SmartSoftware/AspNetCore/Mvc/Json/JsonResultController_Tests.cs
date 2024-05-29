using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shouldly;
using SmartSoftware.Json;
using Xunit;

namespace SmartSoftware.AspNetCore.Mvc.Json;

public class JsonResultController_Tests : AspNetCoreMvcTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<SmartSoftwareJsonOptions>(options =>
        {
            options.OutputDateTimeFormat = "yyyy*MM*dd";
        });

        base.ConfigureServices(services);
    }

    [Fact]
    public async Task DateFormatString_Test()
    {
        var time = await GetResponseAsStringAsync(
            "/api/json-result-test/json-result-action"
        );

        time.ShouldContain("2019*01*01");
    }
}
