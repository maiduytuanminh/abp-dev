using System;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

public class ApplicationConfigurationContributorContext : IServiceProviderAccessor
{
    public IServiceProvider ServiceProvider { get; }

    public ApplicationConfigurationDto ApplicationConfiguration { get; }

    public ApplicationConfigurationContributorContext(IServiceProvider serviceProvider, ApplicationConfigurationDto applicationConfiguration)
    {
        ServiceProvider = serviceProvider;
        ApplicationConfiguration = applicationConfiguration;
    }
}
