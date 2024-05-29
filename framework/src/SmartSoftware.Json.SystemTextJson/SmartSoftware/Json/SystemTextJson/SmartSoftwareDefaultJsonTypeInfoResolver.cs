using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Options;

namespace SmartSoftware.Json.SystemTextJson;

public class SmartSoftwareDefaultJsonTypeInfoResolver : DefaultJsonTypeInfoResolver
{
    public SmartSoftwareDefaultJsonTypeInfoResolver(IOptions<SmartSoftwareSystemTextJsonSerializerModifiersOptions> options)
    {
        foreach (var modifier in options.Value.Modifiers)
        {
            Modifiers.Add(modifier);
        }
    }
}
