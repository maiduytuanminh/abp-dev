using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.AspNetCore.MultiTenancy;

public static class TenantResolveContextExtensions
{
    public static SmartSoftwareAspNetCoreMultiTenancyOptions GetSmartSoftwareAspNetCoreMultiTenancyOptions(this ITenantResolveContext context)
    {
        return context.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareAspNetCoreMultiTenancyOptions>>().Value;
    }
}
