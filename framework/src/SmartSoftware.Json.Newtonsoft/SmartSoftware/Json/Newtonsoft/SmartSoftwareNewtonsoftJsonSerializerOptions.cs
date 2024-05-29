using Newtonsoft.Json;

namespace SmartSoftware.Json.Newtonsoft;

public class SmartSoftwareNewtonsoftJsonSerializerOptions
{
    public JsonSerializerSettings JsonSerializerSettings { get; }

    public SmartSoftwareNewtonsoftJsonSerializerOptions()
    {
        JsonSerializerSettings = new JsonSerializerSettings();
    }
}
