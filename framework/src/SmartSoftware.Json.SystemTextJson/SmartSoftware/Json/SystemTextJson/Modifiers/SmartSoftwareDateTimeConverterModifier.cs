using System;
using System.Linq;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Json.SystemTextJson.JsonConverters;
using SmartSoftware.Reflection;
using SmartSoftware.Timing;

namespace SmartSoftware.Json.SystemTextJson.Modifiers;

public class SmartSoftwareDateTimeConverterModifier
{
    private IServiceProvider _serviceProvider = default!;

    public Action<JsonTypeInfo> CreateModifyAction(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        return Modify;
    }

    private void Modify(JsonTypeInfo jsonTypeInfo)
    {
        if (ReflectionHelper.GetAttributesOfMemberOrDeclaringType<DisableDateTimeNormalizationAttribute>(jsonTypeInfo.Type).Any())
        {
            return;
        }

        foreach (var property in jsonTypeInfo.Properties.Where(x => x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(DateTime?)))
        {
            if (property.AttributeProvider == null ||
                !property.AttributeProvider.GetCustomAttributes(typeof(DisableDateTimeNormalizationAttribute), false).Any())
            {
                property.CustomConverter = property.PropertyType == typeof(DateTime)
                    ? _serviceProvider.GetRequiredService<SmartSoftwareDateTimeConverter>()
                    : _serviceProvider.GetRequiredService<SmartSoftwareNullableDateTimeConverter>();
            }
        }
    }
}
