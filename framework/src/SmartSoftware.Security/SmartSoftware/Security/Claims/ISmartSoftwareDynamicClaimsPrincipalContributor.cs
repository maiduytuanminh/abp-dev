using System.Threading.Tasks;

namespace SmartSoftware.Security.Claims;

public interface ISmartSoftwareDynamicClaimsPrincipalContributor
{
    Task ContributeAsync(SmartSoftwareClaimsPrincipalContributorContext context);
}
