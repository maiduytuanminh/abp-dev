using System;
using JetBrains.Annotations;

namespace SmartSoftware.Localization;

public interface IInheritedResourceTypesProvider
{
    [NotNull]
    Type[] GetInheritedResourceTypes();
}
