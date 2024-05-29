using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SmartSoftware.IdentityServer;

public static class SmartSoftwareIdentityServerServiceCollectionExtensions
{
    public static void AddSmartSoftwareStrictRedirectUriValidator(this IServiceCollection services)
    {
        services.Replace(ServiceDescriptor.Transient<IRedirectUriValidator, SmartSoftwareStrictRedirectUriValidator>());
    }

    public static void AddSmartSoftwareClientConfigurationValidator(this IServiceCollection services)
    {
        services.Replace(ServiceDescriptor.Transient<IClientConfigurationValidator, SmartSoftwareClientConfigurationValidator>());
    }

    public static void AddSmartSoftwareWildcardSubdomainCorsPolicyService(this IServiceCollection services)
    {
        services.Replace(ServiceDescriptor.Transient<ICorsPolicyService, SmartSoftwareWildcardSubdomainCorsPolicyService>());
    }
}
