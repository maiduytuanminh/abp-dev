using JetBrains.Annotations;
using SmartSoftware.Localization;

namespace SmartSoftware.Features;

public interface IFeatureDefinitionContext
{
    FeatureGroupDefinition AddGroup([NotNull] string name, ILocalizableString? displayName = null);

    FeatureGroupDefinition? GetGroupOrNull(string name);

    void RemoveGroup(string name);
}
