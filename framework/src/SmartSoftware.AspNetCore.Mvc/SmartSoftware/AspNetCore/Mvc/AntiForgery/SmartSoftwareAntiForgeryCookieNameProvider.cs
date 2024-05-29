using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.AntiForgery;

public class SmartSoftwareAntiForgeryCookieNameProvider : ITransientDependency
{
    private readonly IOptionsMonitor<CookieAuthenticationOptions> _namedOptionsAccessor;
    private readonly SmartSoftwareAntiForgeryOptions _ssAntiForgeryOptions;

    public SmartSoftwareAntiForgeryCookieNameProvider(
        IOptionsMonitor<CookieAuthenticationOptions> namedOptionsAccessor,
        IOptions<SmartSoftwareAntiForgeryOptions> ssAntiForgeryOptions)
    {
        _namedOptionsAccessor = namedOptionsAccessor;
        _ssAntiForgeryOptions = ssAntiForgeryOptions.Value;
    }

    public virtual string? GetAuthCookieNameOrNull()
    {
        if (_ssAntiForgeryOptions.AuthCookieSchemaName == null)
        {
            return null;
        }

        return _namedOptionsAccessor.Get(_ssAntiForgeryOptions.AuthCookieSchemaName)?.Cookie?.Name;
    }

    public virtual string? GetAntiForgeryCookieNameOrNull()
    {
        return _ssAntiForgeryOptions.TokenCookie.Name;
    }
}
