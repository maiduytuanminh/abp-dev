using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.Reflection;

namespace SmartSoftware.Serialization;

public class SmartSoftwareSerializationModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnExposing(onServiceExposingContext =>
        {
            //Register types for IObjectSerializer<T> if implements
            onServiceExposingContext.ExposedTypes.AddRange(
                ReflectionHelper.GetImplementedGenericTypes(
                    onServiceExposingContext.ImplementationType,
                    typeof(IObjectSerializer<>)
                ).ConvertAll(t => new ServiceIdentifier(t))
            );
        });
    }
}
