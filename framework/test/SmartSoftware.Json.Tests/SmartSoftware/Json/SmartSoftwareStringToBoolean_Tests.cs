using System.Text.Json;
using Shouldly;
using SmartSoftware.Json.SystemTextJson.JsonConverters;
using Xunit;

namespace SmartSoftware.Json;

public class SmartSoftwareStringToBoolean_Tests
{
    [Fact]
    public void Test_Read()
    {
            var options = new JsonSerializerOptions()
            {
                Converters =
                {
                    new SmartSoftwareStringToBooleanConverter()
                }
            };

            var testClass = JsonSerializer.Deserialize<TestClass>("{\"Enabled\": \"TrUe\"}", options);
            testClass.ShouldNotBeNull();
            testClass.Enabled.ShouldBe(true);

            testClass = JsonSerializer.Deserialize<TestClass>("{\"Enabled\": true}", options);
            testClass.ShouldNotBeNull();
            testClass.Enabled.ShouldBe(true);
        }

    [Fact]
    public void Test_Write()
    {
            var options = new JsonSerializerOptions()
            {
                Converters =
                {
                    new SmartSoftwareStringToBooleanConverter()
                }
            };

            var testClassJson = JsonSerializer.Serialize(new TestClass()
            {
                Enabled = true
            });

            testClassJson.ShouldBe("{\"Enabled\":true}");
        }

    class TestClass
    {
        public bool Enabled { get; set; }
    }
}