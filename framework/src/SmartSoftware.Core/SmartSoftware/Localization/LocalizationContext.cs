using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Localization;

public class LocalizationContext : IServiceProviderAccessor
{
    public IServiceProvider ServiceProvider { get; }

    public IStringLocalizerFactory LocalizerFactory { get; }

    public LocalizationContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        LocalizerFactory = ServiceProvider.GetRequiredService<IStringLocalizerFactory>();
    }
}
