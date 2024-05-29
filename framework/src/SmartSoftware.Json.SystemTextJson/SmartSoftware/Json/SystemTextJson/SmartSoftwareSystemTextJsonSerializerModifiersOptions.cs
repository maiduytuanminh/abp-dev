using System;
using System.Collections.Generic;
using System.Text.Json.Serialization.Metadata;
using SmartSoftware.Json.SystemTextJson.Modifiers;


namespace SmartSoftware.Json.SystemTextJson;

public class SmartSoftwareSystemTextJsonSerializerModifiersOptions
{
    public List<Action<JsonTypeInfo>> Modifiers { get; }

    public SmartSoftwareSystemTextJsonSerializerModifiersOptions()
    {
        Modifiers = new List<Action<JsonTypeInfo>>
        {
            SmartSoftwareIncludeExtraPropertiesModifiers.Modify,
        };
    }
}
