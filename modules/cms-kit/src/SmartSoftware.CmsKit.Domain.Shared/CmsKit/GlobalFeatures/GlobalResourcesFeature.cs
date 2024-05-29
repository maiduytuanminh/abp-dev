using JetBrains.Annotations;
using SmartSoftware.GlobalFeatures;

namespace SmartSoftware.CmsKit.GlobalFeatures;

[GlobalFeatureName(Name)]
public class GlobalResourcesFeature : GlobalFeature
{
    public const string Name = "CmsKit.GlobalResources";

    internal GlobalResourcesFeature(
        [NotNull] GlobalCmsKitFeatures cmsKit
    ) : base(cmsKit)
    {
    }
}