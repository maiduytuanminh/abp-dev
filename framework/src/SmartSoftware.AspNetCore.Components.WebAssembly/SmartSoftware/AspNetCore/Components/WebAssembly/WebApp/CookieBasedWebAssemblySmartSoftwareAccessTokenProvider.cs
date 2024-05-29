using System.Threading.Tasks;
using SmartSoftware.Http.Client.Authentication;

namespace SmartSoftware.AspNetCore.Components.WebAssembly.WebApp;

public class CookieBasedWebAssemblySmartSoftwareAccessTokenProvider : ISmartSoftwareAccessTokenProvider
{
    public virtual Task<string?> GetTokenAsync()
    {
        return Task.FromResult<string?>(null);
    }
}
