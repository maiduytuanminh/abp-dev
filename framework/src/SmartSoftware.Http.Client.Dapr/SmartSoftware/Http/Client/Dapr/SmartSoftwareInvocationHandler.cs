using System;
using Dapr.Client;
using Microsoft.Extensions.Options;
using SmartSoftware.Dapr;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Http.Client.Dapr;

public class SmartSoftwareInvocationHandler : InvocationHandler, ITransientDependency
{
    public SmartSoftwareInvocationHandler(IOptions<SmartSoftwareDaprOptions> daprOptions)
    {
        if (!daprOptions.Value.HttpEndpoint.IsNullOrWhiteSpace())
        {
            DaprEndpoint = daprOptions.Value.HttpEndpoint!;
        }
    }
}
