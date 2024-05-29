using SmartSoftware.DependencyInjection;
using SmartSoftware.Json;
using SmartSoftware.Validation.StringValues;

namespace SmartSoftware.FeatureManagement;

public class StringValueTypeSerializer : ITransientDependency
{
    protected IJsonSerializer JsonSerializer { get; }

    public StringValueTypeSerializer(IJsonSerializer jsonSerializer)
    {
        JsonSerializer = jsonSerializer;
    }

    public virtual string Serialize(IStringValueType stringValueType)
    {
        return JsonSerializer.Serialize(stringValueType);
    }

    public virtual IStringValueType Deserialize(string value)
    {
        return JsonSerializer.Deserialize<IStringValueType>(value);
    }
}
