using System;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.Web.DependencyInjection;

public class ComponentsClientScopeServiceProviderAccessor :
    IClientScopeServiceProviderAccessor,
    ISingletonDependency
{
    public IServiceProvider ServiceProvider { get; set; } = default!;
}
