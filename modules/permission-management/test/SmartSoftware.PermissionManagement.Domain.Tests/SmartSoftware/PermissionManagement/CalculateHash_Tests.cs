using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Shouldly;
using SmartSoftware.Json.SystemTextJson.Modifiers;
using Xunit;

namespace SmartSoftware.PermissionManagement;

public class CalculateHash_Tests: PermissionTestBase
{
    [Fact]
    public void Test()
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers =
                {
                    new SmartSoftwareIgnorePropertiesModifiers<PermissionGroupDefinitionRecord, Guid>().CreateModifyAction(x => x.Id),
                    new SmartSoftwareIgnorePropertiesModifiers<PermissionDefinitionRecord, Guid>().CreateModifyAction(x => x.Id)
                }
            }
        };
        var id = Guid.NewGuid();
        var json = JsonSerializer.Serialize(new List<PermissionGroupDefinitionRecord>()
            {
                new PermissionGroupDefinitionRecord(id, "Test", "Test")
            },
            jsonSerializerOptions);
        json.ShouldNotContain("\"Id\"");
        json.ShouldNotContain(id.ToString("D"));
        json = JsonSerializer.Serialize(new List<PermissionDefinitionRecord>()
            {
                new PermissionDefinitionRecord(id, "Test", "Test", "Test", "Test")
            },
            jsonSerializerOptions);
        json.ShouldNotContain("\"Id\"");
        json.ShouldNotContain(id.ToString("D"));
    }
}
