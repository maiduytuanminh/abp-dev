using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.UI.Navigation;

public interface IMenuConfigurationContext : IServiceProviderAccessor
{
    ApplicationMenu Menu { get; }

    IAuthorizationService AuthorizationService { get; }

    IStringLocalizerFactory StringLocalizerFactory { get; }
}
