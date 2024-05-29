using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmartSoftware.Identity;
using SmartSoftware.Security.Claims;
using SmartSoftware.Users;

namespace SmartSoftware.Account;

[Authorize]
public class DynamicClaimsAppService : IdentityAppServiceBase, IDynamicClaimsAppService
{
    protected IdentityDynamicClaimsPrincipalContributorCache IdentityDynamicClaimsPrincipalContributorCache { get; }
    protected ISmartSoftwareClaimsPrincipalFactory SmartSoftwareClaimsPrincipalFactory { get; }
    protected ICurrentPrincipalAccessor PrincipalAccessor { get; }

    public DynamicClaimsAppService(
        IdentityDynamicClaimsPrincipalContributorCache identityDynamicClaimsPrincipalContributorCache,
        ISmartSoftwareClaimsPrincipalFactory ssClaimsPrincipalFactory,
        ICurrentPrincipalAccessor principalAccessor)
    {
        IdentityDynamicClaimsPrincipalContributorCache = identityDynamicClaimsPrincipalContributorCache;
        SmartSoftwareClaimsPrincipalFactory = ssClaimsPrincipalFactory;
        PrincipalAccessor = principalAccessor;
    }

    public virtual async Task RefreshAsync()
    {
        await IdentityDynamicClaimsPrincipalContributorCache.ClearAsync(CurrentUser.GetId(), CurrentUser.TenantId);
        await SmartSoftwareClaimsPrincipalFactory.CreateDynamicAsync(PrincipalAccessor.Principal);
    }
}
