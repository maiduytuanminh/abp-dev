using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Shouldly;
using SmartSoftware.DynamicProxy;
using SmartSoftware.Localization.TestResources.External;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.Localization;

public class SmartSoftwareStringLocalizerFactory_Tests : SmartSoftwareIntegratedTest<SmartSoftwareLocalizationTestModule>
{
    private readonly IStringLocalizerFactory _factory;

    public SmartSoftwareStringLocalizerFactory_Tests()
    {
        _factory = GetRequiredService<IStringLocalizerFactory>();
    }

    [Fact]
    public void Factory_Type_Should_Be_SmartSoftwareStringLocalizerFactory()
    {
        ProxyHelper.UnProxy(_factory).ShouldBeOfType<SmartSoftwareStringLocalizerFactory>();
    }
    
    [Fact]
    public void Should_Create_Resource_By_Name()
    {
        using (CultureHelper.Use("en"))
        {
            var localizer = _factory.CreateByResourceNameOrNull("Test");
            localizer.ShouldNotBeNull();
            localizer["CarPlural"].Value.ShouldBe("Cars");
        }
    }
    
    [Fact]
    public async Task Should_Create_Resource_By_Name_Async()
    {
        using (CultureHelper.Use("en"))
        {
            var localizer = await _factory.CreateByResourceNameOrNullAsync("Test");
            localizer.ShouldNotBeNull();
            localizer["CarPlural"].Value.ShouldBe("Cars");
        }
    }

    [Fact]
    public void Should_Throw_Exception_For_Unknown_Resource_Names()
    {
        Assert.Throws<SmartSoftwareException>(
            () => _factory.CreateByResourceName("UnknownResourceName")
        );
    }

    [Fact]
    public async Task Should_Throw_Exception_For_Unknown_Resource_Names_Async()
    {
        await Assert.ThrowsAsync<SmartSoftwareException>(
            async () => await _factory.CreateByResourceNameAsync("UnknownResourceName")
        );
    }

    [Fact]
    public void Should_Create_External_Resource_By_Name()
    {
        using (CultureHelper.Use("en"))
        {
            var localizer = _factory.CreateByResourceNameOrNull(TestExternalLocalizationStore.TestExternalResourceNames.ExternalResource1);
            localizer.ShouldNotBeNull();
            localizer["CarPlural"].Value.ShouldBe("CarPlural");
            
            var localizer2 = _factory.CreateByResourceNameOrNull(TestExternalLocalizationStore.TestExternalResourceNames.ExternalResource2);
            localizer2.ShouldNotBeNull();
            localizer2["CarPlural"].Value.ShouldBe("CarPlural");
        }
    }

    [Fact]
    public async Task Should_Create_External_Resource_By_Name_Async()
    {
        using (CultureHelper.Use("en"))
        {
            var localizer = await _factory.CreateByResourceNameOrNullAsync(TestExternalLocalizationStore.TestExternalResourceNames.ExternalResource1);
            localizer.ShouldNotBeNull();
            localizer["CarPlural"].Value.ShouldBe("CarPlural");
            
            var localizer2 = await _factory.CreateByResourceNameOrNullAsync(TestExternalLocalizationStore.TestExternalResourceNames.ExternalResource2);
            localizer2.ShouldNotBeNull();
            localizer2["CarPlural"].Value.ShouldBe("CarPlural");
        }
    }
}