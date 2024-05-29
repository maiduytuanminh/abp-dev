using JetBrains.Annotations;
using SmartSoftware.GlobalFeatures;

namespace SmartSoftware.CmsKit.GlobalFeatures;

/// <summary>
/// All CmsKit Features are dependent this feature. Do not disable this manually.
/// </summary>
[GlobalFeatureName(Name)]
public class CmsUserFeature : GlobalFeature
{
    public const string Name = "CmsKit.User";

    internal CmsUserFeature([NotNull] GlobalModuleFeatures module) : base(module)
    {
    }
}
