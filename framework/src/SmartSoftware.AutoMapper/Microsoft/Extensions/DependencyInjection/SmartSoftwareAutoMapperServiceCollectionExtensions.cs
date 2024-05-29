using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.AutoMapper;
using SmartSoftware.ObjectMapping;

namespace Microsoft.Extensions.DependencyInjection;

public static class SmartSoftwareAutoMapperServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMapperObjectMapper(this IServiceCollection services)
    {
        return services.Replace(
            ServiceDescriptor.Transient<IAutoObjectMappingProvider, AutoMapperAutoObjectMappingProvider>()
        );
    }

    public static IServiceCollection AddAutoMapperObjectMapper<TContext>(this IServiceCollection services)
    {
        return services.Replace(
            ServiceDescriptor.Transient<IAutoObjectMappingProvider<TContext>, AutoMapperAutoObjectMappingProvider<TContext>>()
        );
    }
}
