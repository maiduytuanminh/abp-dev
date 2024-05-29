using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Shouldly;
using SmartSoftware.Json.SystemTextJson.Modifiers;
using SmartSoftware.Localization;
using Xunit;

namespace SmartSoftware.Json;

public class SmartSoftwareDatetimeToEnum_Tests : SmartSoftwareJsonSystemTextJsonTestBase
{
    [Theory]
    [InlineData("tr", "14.02.2024")]
    [InlineData("en-US", "02/14/2024")]
    [InlineData("en-GB", "14/02/2024")]
    public void Test_Read(string culture, string datetime)
    {
        var options = new JsonSerializerOptions()
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            {
                Modifiers = { new SmartSoftwareDateTimeConverterModifier().CreateModifyAction(ServiceProvider) }
            }
        };

        using(CultureHelper.Use(culture))
        {
            var testClass = JsonSerializer.Deserialize<TestClass>($"{{\"DateTime\": \"{datetime}\", \"NullableDateTime\": \"{datetime}\"}}", options);
            testClass.ShouldNotBeNull();
            testClass.DateTime.ToString(CultureInfo.CurrentCulture).ShouldStartWith(datetime);
            testClass.NullableDateTime.ShouldNotBeNull();
            testClass.NullableDateTime.Value.ToString(CultureInfo.CurrentCulture).ShouldStartWith(datetime);
        }

        using(CultureHelper.Use(culture))
        {
            var testClass = JsonSerializer.Deserialize<TestClass>($"{{\"DateTime\": \"{datetime}\", \"NullableDateTime\": null}}", options);
            testClass.ShouldNotBeNull();
            testClass.DateTime.ToString(CultureInfo.CurrentCulture).ShouldStartWith(datetime);
            testClass.NullableDateTime.ShouldBeNull();
        }
    }

    class TestClass
    {
        public DateTime DateTime { get; set; }

        public DateTime? NullableDateTime { get; set; }
    }
}
