using System.Threading.Tasks;

namespace SmartSoftware.OpenIddict;

public interface ISmartSoftwareOpenIddictClaimsPrincipalHandler
{
    Task HandleAsync(SmartSoftwareOpenIddictClaimsPrincipalHandlerContext context);
}
