using System.Linq;
using System.Reflection;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Components.WebAssembly.WebApp;

public static class WebAppAdditionalAssembliesHelper
{
    public static Assembly[] GetAssemblies<TModule>()
        where TModule : ISmartSoftwareModule
    {
        return SmartSoftwareModuleHelper.FindAllModuleTypes(typeof(TModule), null)
            .Where(t => t.Name.Contains("Blazor") || t.Name.Contains("WebAssembly"))
            .Select(t => t.Assembly)
            .Distinct()
            .ToArray();
    }
}
