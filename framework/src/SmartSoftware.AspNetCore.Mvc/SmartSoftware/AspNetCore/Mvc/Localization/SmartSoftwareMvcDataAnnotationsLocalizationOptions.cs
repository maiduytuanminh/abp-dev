using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace SmartSoftware.AspNetCore.Mvc.Localization;

public class SmartSoftwareMvcDataAnnotationsLocalizationOptions
{
    public IDictionary<Assembly, Type> AssemblyResources { get; }

    public SmartSoftwareMvcDataAnnotationsLocalizationOptions()
    {
        AssemblyResources = new Dictionary<Assembly, Type>();
    }

    public void AddAssemblyResource(
        [NotNull] Type resourceType,
        params Assembly[] assemblies)
    {
        if (assemblies.IsNullOrEmpty())
        {
            assemblies = new[] { resourceType.Assembly };
        }

        foreach (var assembly in assemblies)
        {
            AssemblyResources[assembly] = resourceType;
        }
    }
}
