using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Http.Client.Authentication;

namespace SmartSoftware.Http.Client.IdentityModel.WebAssembly;

[Dependency(ReplaceServices = true)]
public class WebAssemblySmartSoftwareAccessTokenProvider : ISmartSoftwareAccessTokenProvider, ITransientDependency
{
    protected IAccessTokenProvider? AccessTokenProvider { get; }

    public WebAssemblySmartSoftwareAccessTokenProvider(IAccessTokenProvider accessTokenProvider)
    {
        AccessTokenProvider = accessTokenProvider;
    }

    public virtual async Task<string?> GetTokenAsync()
    {
        if (AccessTokenProvider == null)
        {
            return null;
        }

        var result = await AccessTokenProvider.RequestAccessToken();
        if (result.Status != AccessTokenResultStatus.Success)
        {
            return null;
        }

        result.TryGetToken(out var token);
        return token?.Value;
    }
}
