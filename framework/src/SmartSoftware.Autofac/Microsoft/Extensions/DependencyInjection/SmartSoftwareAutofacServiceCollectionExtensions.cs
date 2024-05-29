using System;
using Autofac;
using JetBrains.Annotations;
using SmartSoftware;

namespace Microsoft.Extensions.DependencyInjection;

public static class SmartSoftwareAutofacServiceCollectionExtensions
{
    [NotNull]
    public static ContainerBuilder GetContainerBuilder([NotNull] this IServiceCollection services)
    {
        Check.NotNull(services, nameof(services));

        var builder = services.GetObjectOrNull<ContainerBuilder>();
        if (builder == null)
        {
            throw new SmartSoftwareException($"Could not find ContainerBuilder. Be sure that you have called {nameof(SmartSoftwareAutofacSmartSoftwareApplicationCreationOptionsExtensions.UseAutofac)} method before!");
        }

        return builder;
    }

    public static IServiceProvider BuildAutofacServiceProvider([NotNull] this IServiceCollection services, Action<ContainerBuilder>? builderAction = null)
    {
        return services.BuildServiceProviderFromFactory(builderAction);
    }
}
