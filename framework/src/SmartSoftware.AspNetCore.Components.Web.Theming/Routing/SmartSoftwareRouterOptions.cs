using System.Reflection;

namespace SmartSoftware.AspNetCore.Components.Web.Theming.Routing;

public class SmartSoftwareRouterOptions
{
    public Assembly AppAssembly { get; set; } = default!;

    public RouterAssemblyList AdditionalAssemblies { get; }

    public SmartSoftwareRouterOptions()
    {
        AdditionalAssemblies = new RouterAssemblyList();
    }
}
