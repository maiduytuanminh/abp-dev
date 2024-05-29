using JetBrains.Annotations;
using System.Collections.Generic;

namespace SmartSoftware.CmsKit.Reactions;

public class CmsKitReactionOptions
{
    [NotNull]
    public List<ReactionEntityTypeDefinition> EntityTypes { get; } = new();
}
