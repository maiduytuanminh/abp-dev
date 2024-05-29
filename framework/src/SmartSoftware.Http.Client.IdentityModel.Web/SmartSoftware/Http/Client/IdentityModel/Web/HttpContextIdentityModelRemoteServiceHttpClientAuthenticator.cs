using System.Threading.Tasks;
using IdentityModel.Client;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Http.Client.Authentication;
using SmartSoftware.IdentityModel;

namespace SmartSoftware.Http.Client.IdentityModel.Web;

[Dependency(ReplaceServices = true)]
public class HttpContextIdentityModelRemoteServiceHttpClientAuthenticator : IdentityModelRemoteServiceHttpClientAuthenticator
{
    protected ISmartSoftwareAccessTokenProvider AccessTokenProvider { get; }

    public HttpContextIdentityModelRemoteServiceHttpClientAuthenticator(
        IIdentityModelAuthenticationService identityModelAuthenticationService,
        ISmartSoftwareAccessTokenProvider accessTokenProvider)
        : base(identityModelAuthenticationService)
    {
        AccessTokenProvider = accessTokenProvider;
    }

    public async override Task Authenticate(RemoteServiceHttpClientAuthenticateContext context)
    {
        if (context.RemoteService.GetUseCurrentAccessToken() != false)
        {
            var accessToken = await AccessTokenProvider.GetTokenAsync();
            if (accessToken != null)
            {
                context.Request.SetBearerToken(accessToken);
                return;
            }
        }

        await base.Authenticate(context);
    }
}
