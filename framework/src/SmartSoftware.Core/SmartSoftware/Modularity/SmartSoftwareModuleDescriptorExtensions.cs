using System;
using System.Linq;
using System.Reflection;

namespace SmartSoftware.Modularity;

public static class SmartSoftwareModuleDescriptorExtensions
{
    public static Assembly[] GetAdditionalAssemblies(this ISmartSoftwareModuleDescriptor module)
    {
        return module.AllAssemblies.Length <= 1
            ? Array.Empty<Assembly>()
            : module.AllAssemblies.Where(x => x != module.Assembly).ToArray();
    }
}