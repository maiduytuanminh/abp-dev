using System;
using System.Collections.Generic;

namespace Microsoft.AspNetCore.Routing;

public class SmartSoftwareEndpointRouterOptions
{
    public List<Action<EndpointRouteBuilderContext>> EndpointConfigureActions { get; }

    public SmartSoftwareEndpointRouterOptions()
    {
        EndpointConfigureActions = new List<Action<EndpointRouteBuilderContext>>();
    }
}
