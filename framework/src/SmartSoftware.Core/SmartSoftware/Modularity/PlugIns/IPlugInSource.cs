using System;
using JetBrains.Annotations;

namespace SmartSoftware.Modularity.PlugIns;

public interface IPlugInSource
{
    [NotNull]
    Type[] GetModules();
}
