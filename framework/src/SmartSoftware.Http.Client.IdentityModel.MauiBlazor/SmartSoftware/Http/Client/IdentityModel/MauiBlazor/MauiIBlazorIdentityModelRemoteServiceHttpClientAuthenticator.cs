using System.Threading.Tasks;
using IdentityModel.Client;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Http.Client.Authentication;
using SmartSoftware.IdentityModel;

namespace SmartSoftware.Http.Client.IdentityModel.MauiBlazor;

[Dependency(ReplaceServices = true)]
public class MauiBlazorIdentityModelRemoteServiceHttpClientAuthenticator : IdentityModelRemoteServiceHttpClientAuthenticator
{
    protected ISmartSoftwareAccessTokenProvider AccessTokenProvider { get; }

    public MauiBlazorIdentityModelRemoteServiceHttpClientAuthenticator(
        IIdentityModelAuthenticationService identityModelAuthenticationService,
        ISmartSoftwareAccessTokenProvider ssAccessTokenProvider)
        : base(identityModelAuthenticationService)
    {
        AccessTokenProvider = ssAccessTokenProvider;
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
