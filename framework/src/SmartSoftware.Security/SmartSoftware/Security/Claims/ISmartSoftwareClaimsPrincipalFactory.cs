using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartSoftware.Security.Claims;

public interface ISmartSoftwareClaimsPrincipalFactory
{
    Task<ClaimsPrincipal> CreateAsync(ClaimsPrincipal? existsClaimsPrincipal = null);

    Task<ClaimsPrincipal> CreateDynamicAsync(ClaimsPrincipal? existsClaimsPrincipal = null);
}
