using System.Collections.Generic;
using SmartSoftware.AspNetCore.MultiTenancy;

namespace SmartSoftware.MultiTenancy;

public static class SmartSoftwareMultiTenancyOptionsExtensions
{
    public static void AddDomainTenantResolver(this SmartSoftwareTenantResolveOptions options, string domainFormat)
    {
        options.TenantResolvers.InsertAfter(
            r => r is CurrentUserTenantResolveContributor,
            new DomainTenantResolveContributor(domainFormat)
        );
    }
}
