using System.Threading.Tasks;
using System.Security.Principal;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using SmartSoftware.Identity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Uow;
using IdentityUser = SmartSoftware.Identity.IdentityUser;

namespace SmartSoftware.IdentityServer.AspNetIdentity;

public class SmartSoftwareProfileService : ProfileService<IdentityUser>
{
    protected ICurrentTenant CurrentTenant { get; }

    public SmartSoftwareProfileService(
        IdentityUserManager userManager,
        IUserClaimsPrincipalFactory<IdentityUser> claimsFactory,
        ICurrentTenant currentTenant)
        : base(userManager, claimsFactory)
    {
        CurrentTenant = currentTenant;
    }

    [UnitOfWork]
    public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        using (CurrentTenant.Change(context.Subject.FindTenantId()))
        {
            await base.GetProfileDataAsync(context);
        }
    }

    [UnitOfWork]
    public override async Task IsActiveAsync(IsActiveContext context)
    {
        using (CurrentTenant.Change(context.Subject.FindTenantId()))
        {
            await base.IsActiveAsync(context);
        }
    }

    [UnitOfWork]
    public override Task<bool> IsUserActiveAsync(IdentityUser user)
    {
        return Task.FromResult(user.IsActive);
    }
}
