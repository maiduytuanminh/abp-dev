using System.Collections.Generic;
using System.Security.Claims;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Security.Claims;

namespace SmartSoftware.AspNetCore.Components.WebAssembly;

public class WebAssemblyRemoteCurrentPrincipalAccessor : CurrentPrincipalAccessorBase, ITransientDependency
{
    protected ApplicationConfigurationCache ApplicationConfigurationCache { get; }

    public WebAssemblyRemoteCurrentPrincipalAccessor(ApplicationConfigurationCache applicationConfigurationCache)
    {
        ApplicationConfigurationCache = applicationConfigurationCache;
    }

    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        var applicationConfiguration = ApplicationConfigurationCache.Get();
        if (applicationConfiguration == null || !applicationConfiguration.CurrentUser.IsAuthenticated)
        {
            return new ClaimsPrincipal(new ClaimsIdentity());
        }

        var claims = new List<Claim>()
        {
            new Claim(SmartSoftwareClaimTypes.UserId, applicationConfiguration.CurrentUser.Id.ToString()!),
        };

        if (applicationConfiguration.CurrentUser.TenantId != null)
        {
            claims.Add(new Claim(SmartSoftwareClaimTypes.TenantId, applicationConfiguration.CurrentUser.TenantId.ToString()!));
        }
        if (applicationConfiguration.CurrentUser.ImpersonatorUserId != null)
        {
            claims.Add(new Claim(SmartSoftwareClaimTypes.ImpersonatorUserId, applicationConfiguration.CurrentUser.ImpersonatorUserId.ToString()!));
        }
        if (applicationConfiguration.CurrentUser.ImpersonatorTenantId != null)
        {
            claims.Add(new Claim(SmartSoftwareClaimTypes.ImpersonatorTenantId, applicationConfiguration.CurrentUser.ImpersonatorTenantId.ToString()!));
        }
        if (applicationConfiguration.CurrentUser.ImpersonatorUserName != null)
        {
            claims.Add(new Claim(SmartSoftwareClaimTypes.ImpersonatorUserName, applicationConfiguration.CurrentUser.ImpersonatorUserName));
        }
        if (applicationConfiguration.CurrentUser.ImpersonatorTenantName != null)
        {
            claims.Add(new Claim(SmartSoftwareClaimTypes.ImpersonatorTenantName, applicationConfiguration.CurrentUser.ImpersonatorTenantName));
        }
        if (applicationConfiguration.CurrentUser.UserName != null)
        {
            claims.Add(new Claim(SmartSoftwareClaimTypes.UserName, applicationConfiguration.CurrentUser.UserName));
        }
        if (applicationConfiguration.CurrentUser.Name != null)
        {
            claims.Add(new Claim(SmartSoftwareClaimTypes.Name, applicationConfiguration.CurrentUser.Name));
        }
        if (applicationConfiguration.CurrentUser.SurName != null)
        {
            claims.Add(new Claim(SmartSoftwareClaimTypes.SurName, applicationConfiguration.CurrentUser.SurName));
        }
        if (applicationConfiguration.CurrentUser.Email != null)
        {
            claims.Add(new Claim(SmartSoftwareClaimTypes.Email, applicationConfiguration.CurrentUser.Email));
        }
        if (applicationConfiguration.CurrentUser.EmailVerified)
        {
            claims.Add(new Claim(SmartSoftwareClaimTypes.EmailVerified, applicationConfiguration.CurrentUser.EmailVerified.ToString()));
        }
        if (applicationConfiguration.CurrentUser.PhoneNumber != null)
        {
            claims.Add(new Claim(SmartSoftwareClaimTypes.PhoneNumber, applicationConfiguration.CurrentUser.PhoneNumber));
        }
        if (applicationConfiguration.CurrentUser.PhoneNumberVerified)
        {
            claims.Add(new Claim(SmartSoftwareClaimTypes.PhoneNumberVerified, applicationConfiguration.CurrentUser.PhoneNumberVerified.ToString()));
        }
        if (applicationConfiguration.CurrentUser.SessionId != null)
        {
            claims.Add(new Claim(SmartSoftwareClaimTypes.SessionId, applicationConfiguration.CurrentUser.SessionId));
        }

        if (!applicationConfiguration.CurrentUser.Roles.IsNullOrEmpty())
        {
            foreach (var role in applicationConfiguration.CurrentUser.Roles)
            {
                claims.Add(new Claim(SmartSoftwareClaimTypes.Role, role));
            }
        }

        return new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType: nameof(WebAssemblyRemoteCurrentPrincipalAccessor)));
    }
}
