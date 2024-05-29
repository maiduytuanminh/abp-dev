using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Caching;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Text.Formatting;

namespace SmartSoftware.IdentityServer;

public class SmartSoftwareWildcardSubdomainCorsPolicyService : SmartSoftwareCorsPolicyService
{
    public SmartSoftwareWildcardSubdomainCorsPolicyService(
        IDistributedCache<AllowedCorsOriginsCacheItem> cache,
        IServiceScopeFactory hybridServiceScopeFactory,
        IOptions<IdentityServerOptions> options)
            : base(cache, hybridServiceScopeFactory, options)
    {

    }

    protected override async Task<bool> IsOriginAllowedAsync(string[] allowedOrigins, string origin)
    {
        var isAllowed = await base.IsOriginAllowedAsync(allowedOrigins, origin);
        if (isAllowed)
        {
            return true;
        }

        foreach (var url in allowedOrigins)
        {
            var extractResult = FormattedStringValueExtracter.Extract(origin, url, ignoreCase: true);
            if (extractResult.IsMatch)
            {
                return extractResult.Matches.Aggregate(url, (current, nameValue) => current.Replace($"{{{nameValue.Name}}}", nameValue.Value))
                    .Contains(origin, StringComparison.OrdinalIgnoreCase);
            }

            if (url.Replace("{0}.", "").Contains(origin, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}
