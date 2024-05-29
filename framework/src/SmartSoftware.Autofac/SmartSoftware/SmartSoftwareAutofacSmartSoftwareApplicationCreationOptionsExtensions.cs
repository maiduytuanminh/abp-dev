using Autofac;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Autofac;

namespace SmartSoftware;

public static class SmartSoftwareAutofacSmartSoftwareApplicationCreationOptionsExtensions
{
    public static void UseAutofac(this SmartSoftwareApplicationCreationOptions options)
    {
        options.Services.AddAutofacServiceProviderFactory();
    }

    public static SmartSoftwareAutofacServiceProviderFactory AddAutofacServiceProviderFactory(this IServiceCollection services)
    {
        return services.AddAutofacServiceProviderFactory(new ContainerBuilder());
    }

    public static SmartSoftwareAutofacServiceProviderFactory AddAutofacServiceProviderFactory(this IServiceCollection services, ContainerBuilder containerBuilder)
    {
        var factory = new SmartSoftwareAutofacServiceProviderFactory(containerBuilder);

        services.AddObjectAccessor(containerBuilder);
        services.AddSingleton((IServiceProviderFactory<ContainerBuilder>)factory);

        return factory;
    }
}
