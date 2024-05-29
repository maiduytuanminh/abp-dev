using System.Collections.Generic;
using JetBrains.Annotations;

namespace SmartSoftware.MultiTenancy;

public class SmartSoftwareTenantResolveOptions
{
    [NotNull]
    public List<ITenantResolveContributor> TenantResolvers { get; }

    public SmartSoftwareTenantResolveOptions()
    {
        TenantResolvers = new List<ITenantResolveContributor>();
    }
}
