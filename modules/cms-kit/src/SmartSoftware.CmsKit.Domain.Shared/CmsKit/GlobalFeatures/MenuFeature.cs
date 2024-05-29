using JetBrains.Annotations;
using SmartSoftware.GlobalFeatures;

namespace SmartSoftware.CmsKit.GlobalFeatures;

[GlobalFeatureName(Name)]
public class MenuFeature : GlobalFeature
{
    public const string Name = "CmsKit.Menus";

    internal MenuFeature(
        [NotNull] GlobalCmsKitFeatures cmsKit) : base(cmsKit)
    {

    }
}
