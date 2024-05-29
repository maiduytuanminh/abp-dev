using JetBrains.Annotations;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.MultiTenancy;

public interface ITenantResolveContext : IServiceProviderAccessor
{
    string? TenantIdOrName { get; set; }

    bool Handled { get; set; }
}
