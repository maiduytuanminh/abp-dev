using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.Json.SystemTextJson;
using SmartSoftware.Json.SystemTextJson.Modifiers;
using Xunit;

namespace SmartSoftware.Json;

public class SmartSoftwareIncludeNonPublicPropertiesModifiers_Tests : SmartSoftwareJsonSystemTextJsonTestBase
{
    private readonly IJsonSerializer _jsonSerializer;

    public SmartSoftwareIncludeNonPublicPropertiesModifiers_Tests()
    {
        _jsonSerializer = GetRequiredService<IJsonSerializer>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.Configure<SmartSoftwareSystemTextJsonSerializerModifiersOptions>(options =>
        {
            options.Modifiers.Add(new SmartSoftwareIncludeNonPublicPropertiesModifiers<NonPublicPropertiesClass, string>().CreateModifyAction(x => x.Name));
            options.Modifiers.Add(new SmartSoftwareIncludeNonPublicPropertiesModifiers<NonPublicPropertiesClass, string>().CreateModifyAction(x => x.Age));
        });

        base.AfterAddApplication(services);
    }

    [Fact]
    public void Test()
    {
        var model = new NonPublicPropertiesClass
        {
            Id = "id"
        };
        model.SetName("my-name");
        model.SetAge("42");

        var json = _jsonSerializer.Serialize(model);

        json.ShouldContain("id");
        json.ShouldContain("name");
        json.ShouldContain("age");

        var obj = _jsonSerializer.Deserialize<NonPublicPropertiesClass>(json);
        obj.Id.ShouldBe("id");
        obj.Name.ShouldBe("my-name");
        obj.Age.ShouldBe("42");
    }

    class NonPublicPropertiesClass
    {
        public string Id { get; set; }

        public string Name { get; private set; }

        public string Age { get; protected set; }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetAge(string age)
        {
            Age = age;
        }
    }
}
