using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Data;
using Xunit;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

public class ApplicationConfigurationBuilder_Tests : AspNetCoreMvcTestBase
{
    [Fact]
    public async Task ApplicationConfigurationBuilder_GetAsync()
    {
        var applicationConfigurationBuilder = GetRequiredService<ISmartSoftwareApplicationConfigurationAppService>();

        var config = await applicationConfigurationBuilder.GetAsync(new ApplicationConfigurationRequestOptions());

        config.Auth.ShouldNotBeNull();
        config.Localization.ShouldNotBeNull();
        config.GetProperty("TestKey").ShouldBe("TestValue");
    }
}
