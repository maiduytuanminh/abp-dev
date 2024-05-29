using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SmartSoftware.Json.Newtonsoft;

public class SmartSoftwareCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
{
    private readonly SmartSoftwareDateTimeConverter _dateTimeConverter;

    public SmartSoftwareCamelCasePropertyNamesContractResolver(SmartSoftwareDateTimeConverter dateTimeConverter)
    {
        _dateTimeConverter = dateTimeConverter;

        NamingStrategy = new CamelCaseNamingStrategy
        {
            ProcessDictionaryKeys = false
        };
    }

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);

        if (SmartSoftwareDateTimeConverter.ShouldNormalize(member, property))
        {
            property.Converter = _dateTimeConverter;
        }

        return property;
    }
}
