using System;

namespace SmartSoftware.MultiTenancy.ConfigurationStore;

public class SmartSoftwareDefaultTenantStoreOptions
{
    public TenantConfiguration[] Tenants { get; set; }

    public SmartSoftwareDefaultTenantStoreOptions()
    {
        Tenants = Array.Empty<TenantConfiguration>();
    }
}
