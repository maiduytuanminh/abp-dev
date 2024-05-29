using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartSoftware.Json.SystemTextJson.JsonConverters;

namespace SmartSoftware.EntityFrameworkCore.ValueConverters;

public class SmartSoftwareJsonValueConverter<TPropertyType> : ValueConverter<TPropertyType, string>
{
    public SmartSoftwareJsonValueConverter()
        : base(
            d => SerializeObject(d),
            s => DeserializeObject(s))
    {

    }

    public readonly static JsonSerializerOptions SerializeOptions = new JsonSerializerOptions();

    private static string SerializeObject(TPropertyType d)
    {
        return JsonSerializer.Serialize(d, SerializeOptions);
    }

    public readonly static JsonSerializerOptions DeserializeOptions = new JsonSerializerOptions()
    {
        Converters =
        {
            new ObjectToInferredTypesConverter()
        }
    };

    private static TPropertyType DeserializeObject(string s)
    {
        return JsonSerializer.Deserialize<TPropertyType>(s, DeserializeOptions)!;
    }
}
