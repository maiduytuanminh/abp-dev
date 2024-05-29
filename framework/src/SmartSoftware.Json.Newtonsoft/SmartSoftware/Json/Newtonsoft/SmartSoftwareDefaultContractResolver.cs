using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SmartSoftware.Json.Newtonsoft;

public class SmartSoftwareDefaultContractResolver : DefaultContractResolver
{
    private readonly SmartSoftwareDateTimeConverter _dateTimeConverter;

    public SmartSoftwareDefaultContractResolver(SmartSoftwareDateTimeConverter dateTimeConverter)
    {
        _dateTimeConverter = dateTimeConverter;
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
