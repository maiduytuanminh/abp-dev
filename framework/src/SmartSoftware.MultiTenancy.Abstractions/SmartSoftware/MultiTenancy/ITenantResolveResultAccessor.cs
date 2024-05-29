using JetBrains.Annotations;

namespace SmartSoftware.MultiTenancy;

public interface ITenantResolveResultAccessor
{
    TenantResolveResult? Result { get; set; }
}
