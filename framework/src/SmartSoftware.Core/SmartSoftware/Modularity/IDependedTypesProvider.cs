using System;
using JetBrains.Annotations;

namespace SmartSoftware.Modularity;

public interface IDependedTypesProvider
{
    [NotNull]
    Type[] GetDependedTypes();
}
