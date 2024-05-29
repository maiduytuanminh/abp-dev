using JetBrains.Annotations;
using SmartSoftware.GlobalFeatures;

namespace SmartSoftware.CmsKit.GlobalFeatures;

[GlobalFeatureName(Name)]
public class ReactionsFeature : GlobalFeature
{
    public const string Name = "CmsKit.Reactions";

    internal ReactionsFeature(
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
