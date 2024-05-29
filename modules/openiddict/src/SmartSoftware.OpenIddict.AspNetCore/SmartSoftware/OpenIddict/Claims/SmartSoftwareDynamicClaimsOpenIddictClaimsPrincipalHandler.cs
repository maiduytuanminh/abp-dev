using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Security.Claims;

namespace SmartSoftware.OpenIddict;

public class SmartSoftwareDynamicClaimsOpenIddictClaimsPrincipalHandler: ISmartSoftwareOpenIddictClaimsPrincipalHandler, ITransientDependency
{
    public virtual async Task HandleAsync(SmartSoftwareOpenIddictClaimsPrincipalHandlerContext context)
    {
        var ssClaimsPrincipalFactory = context
            .ScopeServiceProvider
            .GetRequiredService<ISmartSoftwareClaimsPrincipalFactory>();

        await ssClaimsPrincipalFactory.CreateDynamicAsync(context.Principal);
    }
}
