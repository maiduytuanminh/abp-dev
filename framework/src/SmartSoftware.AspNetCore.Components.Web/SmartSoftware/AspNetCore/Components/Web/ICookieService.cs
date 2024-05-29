using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Components.Web;

public interface ICookieService
{
    public ValueTask SetAsync(string key, string value, CookieOptions? options = null);
    public ValueTask<string> GetAsync(string key);
    public ValueTask DeleteAsync(string key, string? path = null);
}
