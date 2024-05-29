using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Authorization;

public interface ISmartSoftwareAuthorizationService : IAuthorizationService, IServiceProviderAccessor
{
    ClaimsPrincipal CurrentPrincipal { get; }
}
