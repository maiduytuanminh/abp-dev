using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Http.Client.Authentication;

namespace SmartSoftware.Http.Client.IdentityModel.Web;

[Dependency(ReplaceServices = true)]
public class HttpContextSmartSoftwareAccessTokenProvider : ISmartSoftwareAccessTokenProvider, ITransientDependency
{
    protected IHttpContextAccessor HttpContextAccessor { get; }

    public HttpContextSmartSoftwareAccessTokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
    }

    public virtual async Task<string?> GetTokenAsync()
    {
        var httpContext = HttpContextAccessor?.HttpContext;
        if (httpContext == null)
        {
            return null;
        }

        return await httpContext.GetTokenAsync("access_token");
    }
}
