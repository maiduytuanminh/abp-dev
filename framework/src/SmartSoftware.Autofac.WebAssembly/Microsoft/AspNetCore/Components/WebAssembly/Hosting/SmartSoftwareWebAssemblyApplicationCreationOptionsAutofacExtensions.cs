using System;
using Autofac;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware;
using SmartSoftware.AspNetCore.Components.WebAssembly;

namespace Microsoft.AspNetCore.Components.WebAssembly.Hosting;

public static class SmartSoftwareWebAssemblyApplicationCreationOptionsAutofacExtensions
{
    public static void UseAutofac(
        [NotNull] this SmartSoftwareWebAssemblyApplicationCreationOptions options,
        Action<ContainerBuilder>? configure = null)
    {
        options.HostBuilder.Services.AddAutofacServiceProviderFactory();
        options.HostBuilder.ConfigureContainer(
            options.HostBuilder.Services.GetSingletonInstance<IServiceProviderFactory<ContainerBuilder>>(),
            configure
        );
    }
}
