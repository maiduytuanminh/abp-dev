using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Shouldly;
using SmartSoftware.BlobStoring.Fakes;
using SmartSoftware.BlobStoring.TestObjects;
using Xunit;

namespace SmartSoftware.BlobStoring;

public class SmartSoftwareBlobStoringOptions_Tests : SmartSoftwareBlobStoringTestBase
{
    private readonly IBlobContainerConfigurationProvider _configurationProvider;

    public SmartSoftwareBlobStoringOptions_Tests()
    {
        _configurationProvider = GetRequiredService<IBlobContainerConfigurationProvider>();
    }

    [Fact]
    public void Should_Property_Set_And_Get_Options_For_Different_Containers()
    {
        var testContainer1Config = _configurationProvider.Get<TestContainer1>();
        testContainer1Config.ProviderType.ShouldBe(typeof(FakeBlobProvider1));
        testContainer1Config.GetConfigurationOrDefault<string>("TestConfig1").ShouldBe("TestValue1");
        testContainer1Config.GetConfigurationOrDefault<string>("TestConfigDefault").ShouldBe("TestValueDefault");

        var testContainer2Config = _configurationProvider.Get<TestContainer2>();
        testContainer2Config.ProviderType.ShouldBe(typeof(FakeBlobProvider2));
        testContainer2Config.GetConfigurationOrNull("TestConfig2").ShouldBe("TestValue2");
        testContainer2Config.GetConfigurationOrNull("TestConfigDefault").ShouldBe("TestValueDefault");
    }

    [Fact]
    public void Should_Fallback_To_Default_Configuration_If_Not_Specialized()
    {
        var config = _configurationProvider.Get<TestContainer3>();
        config.ProviderType.ShouldBe(typeof(FakeBlobProvider1));
        config.GetConfigurationOrNull("TestConfigDefault").ShouldBe("TestValueDefault");
    }
}
