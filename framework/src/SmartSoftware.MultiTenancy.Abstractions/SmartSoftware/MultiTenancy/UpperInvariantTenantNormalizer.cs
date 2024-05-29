using SmartSoftware.DependencyInjection;

namespace SmartSoftware.MultiTenancy;

public class UpperInvariantTenantNormalizer : ITenantNormalizer, ITransientDependency
{
    public virtual string? NormalizeName(string? name)
    {
        return name?.Normalize().ToUpperInvariant();
    }
}
