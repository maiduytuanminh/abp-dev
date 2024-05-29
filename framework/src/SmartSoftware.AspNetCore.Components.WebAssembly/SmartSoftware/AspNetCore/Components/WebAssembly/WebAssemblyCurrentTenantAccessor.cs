using SmartSoftware.DependencyInjection;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.AspNetCore.Components.WebAssembly;

[Dependency(ReplaceServices = true)]
public class WebAssemblyCurrentTenantAccessor : ICurrentTenantAccessor, ISingletonDependency
{
    public BasicTenantInfo? Current { get; set; }
}
