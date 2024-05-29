using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.SignalR;

namespace SmartSoftware.AspNetCore.SignalR;

public class SmartSoftwareHubContext
{
    public IServiceProvider ServiceProvider { get; }

    public Hub Hub { get; }

    public MethodInfo HubMethod { get; }

    public IReadOnlyList<object?> HubMethodArguments { get; }

    public SmartSoftwareHubContext(IServiceProvider serviceProvider, Hub hub, MethodInfo hubMethod, IReadOnlyList<object?> hubMethodArguments)
    {
        ServiceProvider = serviceProvider;
        Hub = hub;
        HubMethod = hubMethod;
        HubMethodArguments = hubMethodArguments;
    }
}
