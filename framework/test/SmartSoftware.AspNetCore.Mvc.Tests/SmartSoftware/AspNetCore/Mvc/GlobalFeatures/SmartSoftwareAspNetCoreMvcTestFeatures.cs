using JetBrains.Annotations;
using SmartSoftware.GlobalFeatures;

namespace SmartSoftware.AspNetCore.Mvc.GlobalFeatures;

[GlobalFeatureName(Name)]
public class SmartSoftwareAspNetCoreMvcTestFeature1 : SmartSoftware.GlobalFeatures.GlobalFeature
{
    public const string Name = "SmartSoftwareAspNetCoreMvcTest.Feature1";

    internal SmartSoftwareAspNetCoreMvcTestFeature1([NotNull] SmartSoftwareAspNetCoreMvcTestFeatures ssAspNetCoreMvcTestFeatures)
        : base(ssAspNetCoreMvcTestFeatures)
    {

    }
}

public class SmartSoftwareAspNetCoreMvcTestFeatures : GlobalModuleFeatures
{
    public const string ModuleName = "SmartSoftwareAspNetCoreMvcTest";

    public SmartSoftwareAspNetCoreMvcTestFeature1 Reactions => GetFeature<SmartSoftwareAspNetCoreMvcTestFeature1>();

    public SmartSoftwareAspNetCoreMvcTestFeatures([NotNull] GlobalFeatureManager featureManager)
        : base(featureManager)
    {
        AddFeature(new SmartSoftwareAspNetCoreMvcTestFeature1(this));
    }
}
