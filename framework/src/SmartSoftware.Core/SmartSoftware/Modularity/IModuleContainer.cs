using System.Collections.Generic;
using JetBrains.Annotations;

namespace SmartSoftware.Modularity;

public interface IModuleContainer
{
    [NotNull]
    IReadOnlyList<ISmartSoftwareModuleDescriptor> Modules { get; }
}
