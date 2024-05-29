using System.Threading.Tasks;
using Microsoft.JSInterop;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.Web;

[Dependency(ReplaceServices = true)]
public class CookieService : ICookieService, ITransientDependency
{
    public IJSRuntime JsRuntime { get; }

    public CookieService(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
    }

    public async ValueTask SetAsync(string key, string value, CookieOptions? options)
    {
        await JsRuntime.InvokeVoidAsync("ss.utils.setCookieValue", key, value, options?.ExpireDate?.ToString("r"), options?.Path, options?.Secure);
    }

    public async ValueTask<string> GetAsync(string key)
    {
        return await JsRuntime.InvokeAsync<string>("ss.utils.getCookieValue", key);
    }

    public async ValueTask DeleteAsync(string key, string? path = null)
    {
        await JsRuntime.InvokeVoidAsync("ss.utils.deleteCookie", key);
    }
}
