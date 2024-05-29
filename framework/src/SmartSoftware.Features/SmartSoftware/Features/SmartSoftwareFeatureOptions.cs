using System.Collections.Generic;
using SmartSoftware.Collections;

namespace SmartSoftware.Features;

public class SmartSoftwareFeatureOptions
{
    public ITypeList<IFeatureDefinitionProvider> DefinitionProviders { get; }

    public ITypeList<IFeatureValueProvider> ValueProviders { get; }

    public HashSet<string> DeletedFeatures { get; }

    public HashSet<string> DeletedFeatureGroups { get; }

    public SmartSoftwareFeatureOptions()
    {
        DefinitionProviders = new TypeList<IFeatureDefinitionProvider>();
        ValueProviders = new TypeList<IFeatureValueProvider>();

        DeletedFeatures = new HashSet<string>();
        DeletedFeatureGroups = new HashSet<string>();
    }
}
