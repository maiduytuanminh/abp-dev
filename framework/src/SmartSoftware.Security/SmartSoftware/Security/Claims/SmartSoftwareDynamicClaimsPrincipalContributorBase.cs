using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Security.Claims;

public abstract class SmartSoftwareDynamicClaimsPrincipalContributorBase : ISmartSoftwareDynamicClaimsPrincipalContributor, ITransientDependency
{
    public abstract Task ContributeAsync(SmartSoftwareClaimsPrincipalContributorContext context);

    protected virtual async Task AddDynamicClaimsAsync(SmartSoftwareClaimsPrincipalContributorContext context, ClaimsIdentity identity, List<SmartSoftwareDynamicClaim> dynamicClaims)
    {
        var options = context.GetRequiredService<IOptions<SmartSoftwareClaimsPrincipalFactoryOptions>>().Value;
        foreach (var map in options.ClaimsMap)
        {
            await MapClaimAsync(identity, dynamicClaims, map.Key, map.Value.ToArray());
        }

        foreach (var claimGroup in dynamicClaims.GroupBy(x => x.Type))
        {
            identity.RemoveAll(claimGroup.First().Type);
            identity.AddClaims(claimGroup.Where(c => c.Value != null).Select(c => new Claim(claimGroup.First().Type, c.Value!)));
        }
    }

    protected virtual Task MapClaimAsync(ClaimsIdentity identity, List<SmartSoftwareDynamicClaim> dynamicClaims, string targetClaimType, params string[] sourceClaimTypes)
    {
        var claims = dynamicClaims.Where(c => sourceClaimTypes.Contains(c.Type)).ToList();
        if (claims.IsNullOrEmpty())
        {
            return Task.CompletedTask;
        }

        dynamicClaims.RemoveAll(claims);
        identity.RemoveAll(targetClaimType);
        identity.AddClaims(claims.Where(c => c.Value != null).Select(c => new Claim(targetClaimType, c.Value!)));

        return Task.CompletedTask;;
    }
}
