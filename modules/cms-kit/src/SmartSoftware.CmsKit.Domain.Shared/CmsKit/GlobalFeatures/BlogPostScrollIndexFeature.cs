using JetBrains.Annotations;
using SmartSoftware.GlobalFeatures;

namespace SmartSoftware.CmsKit.GlobalFeatures;

[GlobalFeatureName(Name)]
public class BlogPostScrollIndexFeature : GlobalFeature
{
    public const string Name = "CmsKit.BlogPost.ScrollIndex";

    internal BlogPostScrollIndexFeature(
        [NotNull] GlobalCmsKitFeatures cmsKit
    ) : base(cmsKit)
    {
    }
}