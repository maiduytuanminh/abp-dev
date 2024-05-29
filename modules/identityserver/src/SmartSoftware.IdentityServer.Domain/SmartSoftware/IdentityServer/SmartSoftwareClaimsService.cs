using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using SmartSoftware.Security.Claims;

namespace SmartSoftware.IdentityServer;

public class SmartSoftwareClaimsService : DefaultClaimsService
{
    protected readonly SmartSoftwareClaimsServiceOptions Options;

    private static readonly string[] AdditionalOptionalClaimNames =
    {
            SmartSoftwareClaimTypes.TenantId,
            SmartSoftwareClaimTypes.ImpersonatorTenantId,
            SmartSoftwareClaimTypes.ImpersonatorUserId,
            SmartSoftwareClaimTypes.Name,
            SmartSoftwareClaimTypes.SurName,
            JwtRegisteredClaimNames.UniqueName,
            JwtClaimTypes.PreferredUserName,
            JwtClaimTypes.GivenName,
            JwtClaimTypes.FamilyName,
        };

    public SmartSoftwareClaimsService(
        IProfileService profile,
        ILogger<DefaultClaimsService> logger,
        IOptions<SmartSoftwareClaimsServiceOptions> options)
        : base(profile, logger)
    {
        Options = options.Value;
    }

    protected override IEnumerable<string> FilterRequestedClaimTypes(IEnumerable<string> claimTypes)
    {
        return base.FilterRequestedClaimTypes(claimTypes)
            .Union(Options.RequestedClaims);
    }

    protected override IEnumerable<Claim> GetOptionalClaims(ClaimsPrincipal subject)
    {
        return base.GetOptionalClaims(subject)
            .Union(GetAdditionalOptionalClaims(subject));
    }

    protected virtual IEnumerable<Claim> GetAdditionalOptionalClaims(ClaimsPrincipal subject)
    {
        foreach (var claimName in AdditionalOptionalClaimNames)
        {
            var claim = subject.FindFirst(claimName);
            if (claim != null)
            {
                yield return claim;
            }
        }
    }
}
