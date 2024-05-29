using SmartSoftware.DependencyInjection;

namespace SmartSoftware.MultiTenancy;

public class NullTenantResolveResultAccessor : ITenantResolveResultAccessor, ISingletonDependency
{
    public TenantResolveResult? Result {
        get => null;
        set { }
    }
}
