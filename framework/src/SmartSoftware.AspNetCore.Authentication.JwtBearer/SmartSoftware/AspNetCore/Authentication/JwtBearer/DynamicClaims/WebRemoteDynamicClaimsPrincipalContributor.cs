using SmartSoftware.DependencyInjection;
using SmartSoftware.Security.Claims;

namespace SmartSoftware.AspNetCore.Authentication.JwtBearer.DynamicClaims;

[DisableConventionalRegistration]
public class WebRemoteDynamicClaimsPrincipalContributor : RemoteDynamicClaimsPrincipalContributorBase<WebRemoteDynamicClaimsPrincipalContributor, WebRemoteDynamicClaimsPrincipalContributorCache>
{

}
