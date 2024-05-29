using System;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.EventBus;

public class MySimpleEventData : IMultiTenant
{
    public int Value { get; set; }

    public Guid? TenantId { get; }

    public MySimpleEventData(int value, Guid? tenantId = null)
    {
        Value = value;
        TenantId = tenantId;
    }
}
