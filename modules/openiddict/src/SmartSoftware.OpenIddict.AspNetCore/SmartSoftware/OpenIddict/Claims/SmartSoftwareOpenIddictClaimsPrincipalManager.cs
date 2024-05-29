using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.OpenIddict;

public class SmartSoftwareOpenIddictClaimsPrincipalManager : ISingletonDependency
{
    protected IServiceScopeFactory ServiceScopeFactory { get; }
    protected IOptions<SmartSoftwareOpenIddictClaimsPrincipalOptions> Options { get; }

    public SmartSoftwareOpenIddictClaimsPrincipalManager(IServiceScopeFactory serviceScopeFactory, IOptions<SmartSoftwareOpenIddictClaimsPrincipalOptions> options)
    {
        ServiceScopeFactory = serviceScopeFactory;
        Options = options;
    }

    public virtual async Task HandleAsync(OpenIddictRequest openIddictRequest, ClaimsPrincipal principal)
    {
        using (var scope = ServiceScopeFactory.CreateScope())
        {
            foreach (var providerType in Options.Value.ClaimsPrincipalHandlers)
            {
                var provider = (ISmartSoftwareOpenIddictClaimsPrincipalHandler)scope.ServiceProvider.GetRequiredService(providerType);
                await provider.HandleAsync(new SmartSoftwareOpenIddictClaimsPrincipalHandlerContext(scope.ServiceProvider, openIddictRequest, principal));
            }
        }
    }
}
