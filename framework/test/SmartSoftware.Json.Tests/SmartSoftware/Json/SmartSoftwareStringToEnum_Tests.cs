using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Shouldly;
using SmartSoftware.Json.SystemTextJson.JsonConverters;
using Xunit;

namespace SmartSoftware.Json;

public class SmartSoftwareStringToEnum_Tests
{
    [Fact]
    public void Test_Read()
    {
        var options = new JsonSerializerOptions()
        {
            Converters =
            {
                new SmartSoftwareStringToEnumFactory()
            }
        };

        var testClass = JsonSerializer.Deserialize<TestClass>("{\"Day\": \"Monday\"}", options);
        testClass.ShouldNotBeNull();
        testClass.Day.ShouldBe(DayOfWeek.Monday);

        testClass = JsonSerializer.Deserialize<TestClass>("{\"Day\": 1}", options);
        testClass.ShouldNotBeNull();
        testClass.Day.ShouldBe(DayOfWeek.Monday);

        var dictionary = JsonSerializer.Deserialize<Dictionary<DayOfWeek, string>>("{\"Monday\":\"Mo\"}", options);
        dictionary.ShouldNotBeNull();
        dictionary.Keys.ShouldContain(DayOfWeek.Monday);
        dictionary.Values.ShouldContain("Mo");

        dictionary = JsonSerializer.Deserialize<Dictionary<DayOfWeek, string>>("{\"1\":\"Mo\"}", options);
        dictionary.ShouldNotBeNull();
        dictionary.Keys.ShouldContain(DayOfWeek.Monday);
        dictionary.Values.ShouldContain("Mo");
    }

    [Fact]
    public void Test_Write()
    {
        var options = new JsonSerializerOptions()
        {
            Converters =
            {
                new SmartSoftwareStringToEnumFactory()
            }
        };

        var testClassJson = JsonSerializer.Serialize(new TestClass()
        {
            Day = DayOfWeek.Monday
        }, options);

        testClassJson.ShouldBe("{\"Day\":1}");

        options = new JsonSerializerOptions()
        {
            Converters =
            {
                new SmartSoftwareStringToEnumFactory(),
                new JsonStringEnumConverter()
            }
        };

        testClassJson = JsonSerializer.Serialize(new TestClass()
        {
            Day = DayOfWeek.Monday
        }, options);

        testClassJson.ShouldBe("{\"Day\":\"Monday\"}");

        testClassJson = JsonSerializer.Serialize(new Dictionary<DayOfWeek, string>
        {
            {DayOfWeek.Monday, "Mo"}
        }, options);

        testClassJson.ShouldBe("{\"Monday\":\"Mo\"}");
    }

    class TestClass
    {
        public DayOfWeek Day { get; set; }
    }
}
