using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Components.Web.Security;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Security.Claims;

namespace SmartSoftware.AspNetCore.Components.MauiBlazor;

public class MauiBlazorCurrentPrincipalAccessor : CurrentPrincipalAccessorBase, ITransientDependency
{
    private SmartSoftwareComponentsClaimsCache ClaimsCache { get; }

    public MauiBlazorCurrentPrincipalAccessor(
        IClientScopeServiceProviderAccessor clientScopeServiceProviderAccessor)
    {
        ClaimsCache = clientScopeServiceProviderAccessor.ServiceProvider.GetRequiredService<SmartSoftwareComponentsClaimsCache>();
    }

    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        return ClaimsCache.Principal;
    }
}