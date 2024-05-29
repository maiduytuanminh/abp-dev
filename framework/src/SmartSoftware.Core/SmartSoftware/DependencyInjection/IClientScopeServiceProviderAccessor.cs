using System;

namespace SmartSoftware.DependencyInjection;

public interface IClientScopeServiceProviderAccessor
{
    IServiceProvider ServiceProvider { get; }
}
