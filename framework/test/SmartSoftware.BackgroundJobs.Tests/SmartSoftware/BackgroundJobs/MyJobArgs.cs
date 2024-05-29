using System;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.BackgroundJobs;

public class MyJobArgs : IMultiTenant
{
    public string Value { get; set; }

    public MyJobArgs()
    {

    }
    

    public MyJobArgs(string value, Guid? tenantId = null)
    {
        Value = value;
        TenantId = tenantId;
    }

    public Guid? TenantId { get; }
}
