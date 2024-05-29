using System.Threading.Tasks;

namespace SmartSoftware.Security.Claims;

public interface ISmartSoftwareClaimsPrincipalContributor
{
    Task ContributeAsync(SmartSoftwareClaimsPrincipalContributorContext context);
}
