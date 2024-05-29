using System.Collections.Generic;

namespace SmartSoftware.Features;

public interface IFeatureValueProviderManager
{
    IReadOnlyList<IFeatureValueProvider> ValueProviders { get; }
}