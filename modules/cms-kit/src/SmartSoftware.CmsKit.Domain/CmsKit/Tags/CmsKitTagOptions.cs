using JetBrains.Annotations;

namespace SmartSoftware.CmsKit.Tags;

public class CmsKitTagOptions
{
    [NotNull]
    public TagEntityTypeDefinitions EntityTypes { get; } = new TagEntityTypeDefinitions();
}
