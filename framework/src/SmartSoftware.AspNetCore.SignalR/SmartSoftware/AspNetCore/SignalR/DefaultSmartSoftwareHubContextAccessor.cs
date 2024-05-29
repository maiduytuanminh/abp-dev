using System;
using System.Threading;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.SignalR;

public class DefaultSmartSoftwareHubContextAccessor : ISmartSoftwareHubContextAccessor, ISingletonDependency
{
    public SmartSoftwareHubContext Context => _currentHubContext.Value!;

    private readonly AsyncLocal<SmartSoftwareHubContext> _currentHubContext = new AsyncLocal<SmartSoftwareHubContext>();

    public virtual IDisposable Change(SmartSoftwareHubContext context)
    {
        var parent = Context;
        _currentHubContext.Value = context;
        return new DisposeAction(() =>
        {
            _currentHubContext.Value = parent;
        });
    }
}
