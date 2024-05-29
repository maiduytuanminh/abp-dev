using Shouldly;
using SmartSoftware.Data;
using SmartSoftware.Json.SystemTextJson;
using SmartSoftware.ObjectExtending;
using Xunit;

namespace SmartSoftware.Json;

public class ExtensibleObjectModifiers_Tests : SmartSoftwareJsonSystemTextJsonTestBase
{
    [Fact]
    public void Should_Modify_Object()
    {
        var jsonSerializer = GetRequiredService<SmartSoftwareSystemTextJsonSerializer>();

        var extensibleObject = jsonSerializer.Deserialize<ExtensibleObject>("{\"ExtraProperties\": {\"Test-Key\":\"Test-Value\"}}");
        extensibleObject.ExtraProperties.ShouldContainKeyAndValue("Test-Key", "Test-Value");

        var bar = jsonSerializer.Deserialize<Bar>("{\"ExtraProperties\": {\"Test-Key\":\"Test-Value\"}}");
        bar.ExtraProperties.ShouldContainKeyAndValue("Test-Key", "Test-Value");
    }
}

public abstract class Foo : IHasExtraProperties
{
    public ExtraPropertyDictionary ExtraProperties { get; protected set; }
}

public class Bar : Foo
{

}
