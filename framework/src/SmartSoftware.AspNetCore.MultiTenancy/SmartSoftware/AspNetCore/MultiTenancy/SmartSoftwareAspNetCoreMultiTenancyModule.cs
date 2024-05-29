using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.AspNetCore.MultiTenancy;

[DependsOn(
    typeof(SmartSoftwareMultiTenancyModule),
    typeof(SmartSoftwareAspNetCoreModule)
    )]
public class SmartSoftwareAspNetCoreMultiTenancyModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareTenantResolveOptions>(options =>
        {
            options.TenantResolvers.Add(new QueryStringTenantResolveContributor());
            options.TenantResolvers.Add(new RouteTenantResolveContributor());
            options.TenantResolvers.Add(new HeaderTenantResolveContributor());
            options.TenantResolvers.Add(new CookieTenantResolveContributor());
        });
    }
}
