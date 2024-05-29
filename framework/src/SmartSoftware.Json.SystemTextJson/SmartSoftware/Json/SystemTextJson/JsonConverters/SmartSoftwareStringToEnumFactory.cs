using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartSoftware.Json.SystemTextJson.JsonConverters;

public class SmartSoftwareStringToEnumFactory : JsonConverterFactory
{
    private readonly JsonNamingPolicy? _namingPolicy;
    private readonly bool _allowIntegerValues;

    public SmartSoftwareStringToEnumFactory()
        : this(namingPolicy: null, allowIntegerValues: true)
    {

    }

    public SmartSoftwareStringToEnumFactory(JsonNamingPolicy? namingPolicy, bool allowIntegerValues)
    {
        _namingPolicy = namingPolicy;
        _allowIntegerValues = allowIntegerValues;
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        return (JsonConverter)Activator.CreateInstance(
            typeof(SmartSoftwareStringToEnumConverter<>).MakeGenericType(typeToConvert),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            new object?[] { _namingPolicy, _allowIntegerValues },
            culture: null)!;
    }
}
