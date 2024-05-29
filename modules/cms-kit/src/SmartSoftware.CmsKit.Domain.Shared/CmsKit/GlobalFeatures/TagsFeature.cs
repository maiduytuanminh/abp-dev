﻿using JetBrains.Annotations;
using SmartSoftware.GlobalFeatures;

namespace SmartSoftware.CmsKit.GlobalFeatures;

[GlobalFeatureName(Name)]
public class TagsFeature : GlobalFeature
{
    public const string Name = "CmsKit.Tags";

    internal TagsFeature(
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
