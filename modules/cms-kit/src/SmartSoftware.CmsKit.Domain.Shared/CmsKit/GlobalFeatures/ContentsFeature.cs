using JetBrains.Annotations;
using SmartSoftware.GlobalFeatures;

namespace SmartSoftware.CmsKit.GlobalFeatures;

[GlobalFeatureName(Name)]
public class ContentsFeature : GlobalFeature
{
    public const string Name = "CmsKit.Contents";

    internal ContentsFeature(
        [NotNull] GlobalCmsKitFeatures cmsKit
    ) : base(cmsKit)
    {

    }

    public override void Enable()
    {
        var userFeature = FeatureManager.Modules.CmsKit().User;
        if (!userFeature.IsEnabled)
        {
            userFeature.Enable();
        }

        base.Enable();
    }
}
