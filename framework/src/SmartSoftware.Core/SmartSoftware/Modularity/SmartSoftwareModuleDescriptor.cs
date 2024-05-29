using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using JetBrains.Annotations;

namespace SmartSoftware.Modularity;

public class SmartSoftwareModuleDescriptor : ISmartSoftwareModuleDescriptor
{
    public Type Type { get; }

    public Assembly Assembly { get; }
    
    public Assembly[] AllAssemblies { get; }

    public ISmartSoftwareModule Instance { get; }

    public bool IsLoadedAsPlugIn { get; }

    public IReadOnlyList<ISmartSoftwareModuleDescriptor> Dependencies => _dependencies.ToImmutableList();
    private readonly List<ISmartSoftwareModuleDescriptor> _dependencies;

    public SmartSoftwareModuleDescriptor(
        [NotNull] Type type,
        [NotNull] ISmartSoftwareModule instance,
        bool isLoadedAsPlugIn)
    {
        Check.NotNull(type, nameof(type));
        Check.NotNull(instance, nameof(instance));
        SmartSoftwareModule.CheckSmartSoftwareModuleType(type);

        if (!type.GetTypeInfo().IsAssignableFrom(instance.GetType()))
        {
            throw new ArgumentException($"Given module instance ({instance.GetType().AssemblyQualifiedName}) is not an instance of given module type: {type.AssemblyQualifiedName}");
        }

        Type = type;
        Assembly = type.Assembly;
        AllAssemblies = SmartSoftwareModuleHelper.GetAllAssemblies(type);
        Instance = instance;
        IsLoadedAsPlugIn = isLoadedAsPlugIn;

        _dependencies = new List<ISmartSoftwareModuleDescriptor>();
    }

    public void AddDependency(ISmartSoftwareModuleDescriptor descriptor)
    {
        _dependencies.AddIfNotContains(descriptor);
    }

    public override string ToString()
    {
        return $"[SmartSoftwareModuleDescriptor {Type.FullName}]";
    }
}
