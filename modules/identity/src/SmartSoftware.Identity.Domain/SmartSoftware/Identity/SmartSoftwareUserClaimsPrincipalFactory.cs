using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Principal;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Security.Claims;
using SmartSoftware.Uow;

namespace SmartSoftware.Identity;

public class SmartSoftwareUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser, IdentityRole>,
    ITransientDependency
{
    protected ICurrentPrincipalAccessor CurrentPrincipalAccessor { get; }
    protected ISmartSoftwareClaimsPrincipalFactory SmartSoftwareClaimsPrincipalFactory { get; }

    public SmartSoftwareUserClaimsPrincipalFactory(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options,
        ICurrentPrincipalAccessor currentPrincipalAccessor,
        ISmartSoftwareClaimsPrincipalFactory ssClaimsPrincipalFactory)
        : base(
            userManager,
            roleManager,
            options)
    {
        CurrentPrincipalAccessor = currentPrincipalAccessor;
        SmartSoftwareClaimsPrincipalFactory = ssClaimsPrincipalFactory;
    }

    [UnitOfWork]
    public async override Task<ClaimsPrincipal> CreateAsync(IdentityUser user)
    {
        var principal = await base.CreateAsync(user);
        var identity = principal.Identities.First();

        if (user.TenantId.HasValue)
        {
            identity.AddIfNotContains(new Claim(SmartSoftwareClaimTypes.TenantId, user.TenantId.ToString()));
        }

        if (!user.Name.IsNullOrWhiteSpace())
        {
            identity.AddIfNotContains(new Claim(SmartSoftwareClaimTypes.Name, user.Name));
        }

        if (!user.Surname.IsNullOrWhiteSpace())
        {
            identity.AddIfNotContains(new Claim(SmartSoftwareClaimTypes.SurName, user.Surname));
        }

        if (!user.PhoneNumber.IsNullOrWhiteSpace())
        {
            identity.AddIfNotContains(new Claim(SmartSoftwareClaimTypes.PhoneNumber, user.PhoneNumber));
        }

        identity.AddIfNotContains(
            new Claim(SmartSoftwareClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed.ToString()));

        if (!user.Email.IsNullOrWhiteSpace())
        {
            identity.AddIfNotContains(new Claim(SmartSoftwareClaimTypes.Email, user.Email));
        }

        identity.AddIfNotContains(new Claim(SmartSoftwareClaimTypes.EmailVerified, user.EmailConfirmed.ToString()));

        using (CurrentPrincipalAccessor.Change(identity))
        {
            await SmartSoftwareClaimsPrincipalFactory.CreateAsync(principal);
        }

        return principal;
    }
}
