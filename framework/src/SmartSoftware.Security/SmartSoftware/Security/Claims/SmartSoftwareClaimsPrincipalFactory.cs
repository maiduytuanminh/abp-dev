using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Security.Claims;

public class SmartSoftwareClaimsPrincipalFactory : ISmartSoftwareClaimsPrincipalFactory, ITransientDependency
{
    public static string AuthenticationType => "SmartSoftware.Application";

    protected IServiceScopeFactory ServiceScopeFactory { get; }
    protected SmartSoftwareClaimsPrincipalFactoryOptions Options { get; }

    public SmartSoftwareClaimsPrincipalFactory(
        IServiceScopeFactory serviceScopeFactory,
        IOptions<SmartSoftwareClaimsPrincipalFactoryOptions> ssClaimOptions)
    {
        ServiceScopeFactory = serviceScopeFactory;
        Options = ssClaimOptions.Value;
    }

    public virtual async Task<ClaimsPrincipal> CreateAsync(ClaimsPrincipal? existsClaimsPrincipal = null)
    {
        return await InternalCreateAsync(Options, existsClaimsPrincipal, false);
    }

    public virtual async Task<ClaimsPrincipal> CreateDynamicAsync(ClaimsPrincipal? existsClaimsPrincipal = null)
    {
        return await InternalCreateAsync(Options, existsClaimsPrincipal, true);
    }

    public virtual async Task<ClaimsPrincipal> InternalCreateAsync(SmartSoftwareClaimsPrincipalFactoryOptions options, ClaimsPrincipal? existsClaimsPrincipal = null, bool isDynamic = false)
    {
        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var claimsPrincipal = existsClaimsPrincipal ?? new ClaimsPrincipal(new ClaimsIdentity(
                AuthenticationType,
                SmartSoftwareClaimTypes.UserName,
                SmartSoftwareClaimTypes.Role));

            var context = new SmartSoftwareClaimsPrincipalContributorContext(claimsPrincipal, scope.ServiceProvider);

            if (!isDynamic)
            {
                foreach (var contributorType in options.Contributors)
                {
                    var contributor = (ISmartSoftwareClaimsPrincipalContributor)scope.ServiceProvider.GetRequiredService(contributorType);
                    await contributor.ContributeAsync(context);
                }
            }
            else
            {
                foreach (var contributorType in options.DynamicContributors)
                {
                    var contributor = (ISmartSoftwareDynamicClaimsPrincipalContributor)scope.ServiceProvider.GetRequiredService(contributorType);
                    await contributor.ContributeAsync(context);
                }
            }

            return context.ClaimsPrincipal;
        }
    }
}
