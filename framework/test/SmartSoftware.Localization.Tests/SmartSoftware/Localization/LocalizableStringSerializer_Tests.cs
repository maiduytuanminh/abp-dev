using Shouldly;
using SmartSoftware.Localization.TestResources.Source;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.Localization;

public class LocalizableStringSerializer_Tests : SmartSoftwareIntegratedTest<SmartSoftwareLocalizationTestModule>
{
    private readonly ILocalizableStringSerializer _serializer;
    
    public LocalizableStringSerializer_Tests()
    {
        _serializer = GetRequiredService<ILocalizableStringSerializer>();
    }

    [Fact]
    public void Serialize_FixedLocalizableString()
    {
        _serializer
            .Serialize(new FixedLocalizableString(""))
            .ShouldBe("F:");
        
        _serializer
            .Serialize(new FixedLocalizableString("Hello World"))
            .ShouldBe("F:Hello World");
    }
    
    [Fact]
    public void Serialize_LocalizableString()
    {
        _serializer
            .Serialize(new LocalizableString(typeof(LocalizationTestResource),"Car"))
            .ShouldBe("L:Test,Car");
    }
    
    [Fact]
    public void Deserialize_FixedLocalizableString()
    {
        _serializer
            .Deserialize("")
            .ShouldBeOfType<FixedLocalizableString>()
            .Value.ShouldBe("");
        
        _serializer
            .Deserialize("Hello")
            .ShouldBeOfType<FixedLocalizableString>()
            .Value.ShouldBe("Hello");

        _serializer
            .Deserialize("F:Hello")
            .ShouldBeOfType<FixedLocalizableString>()
            .Value.ShouldBe("Hello");
    }
    
    [Fact]
    public void Deserialize_LocalizableString()
    {
        var localizableString = _serializer
            .Deserialize("L:Test,Car")
            .ShouldBeOfType<LocalizableString>();
        localizableString.ResourceName.ShouldBe("Test");
        localizableString.Name.ShouldBe("Car");

        Assert.Throws<SmartSoftwareException>(() =>
        {
            _serializer.Deserialize("L:Test");
        });
        
        Assert.Throws<SmartSoftwareException>(() =>
        {
            _serializer.Deserialize("L:Test, ");
        });
    }
}