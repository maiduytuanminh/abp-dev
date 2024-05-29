using SmartSoftware.AspNetCore.MultiTenancy;

namespace Microsoft.AspNetCore.Builder;

public static class SmartSoftwareAspNetCoreMultiTenancyApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app)
    {
        return app
            .UseMiddleware<MultiTenancyMiddleware>();
    }
}
