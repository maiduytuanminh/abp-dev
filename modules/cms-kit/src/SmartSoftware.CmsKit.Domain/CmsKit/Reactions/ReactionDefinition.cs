using JetBrains.Annotations;
using System;
using SmartSoftware;
using SmartSoftware.Localization;

namespace SmartSoftware.CmsKit.Reactions;

public class ReactionDefinition
{
    [NotNull]
    public string Name { get; }

    [CanBeNull]
    public ILocalizableString DisplayName { get; set; }

    public ReactionDefinition(
        [NotNull] string name,
        [CanBeNull] ILocalizableString displayName = null)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        DisplayName = displayName;
    }
}
