using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace SmartSoftware.AspNetCore.Security.Claims;

public class SmartSoftwareClaimsTransformation : IClaimsTransformation
{
    protected IOptions<SmartSoftwareClaimsMapOptions> SmartSoftwareClaimsMapOptions { get; }

    public SmartSoftwareClaimsTransformation(IOptions<SmartSoftwareClaimsMapOptions> ssClaimsMapOptions)
    {
        SmartSoftwareClaimsMapOptions = ssClaimsMapOptions;
    }

    public virtual Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var mapClaims = principal.Claims.Where(claim => SmartSoftwareClaimsMapOptions.Value.Maps.Keys.Contains(claim.Type));

        principal.AddIdentity(new ClaimsIdentity(mapClaims.Select(
                    claim => new Claim(
                        SmartSoftwareClaimsMapOptions.Value.Maps[claim.Type](),
                        claim.Value,
                        claim.ValueType,
                        claim.Issuer
                    )
                )
            )
        );

        return Task.FromResult(principal);
    }
}
