using System;

namespace SmartSoftware.CmsKit.Public.Reactions;

[Serializable]
public class ReactionWithSelectionDto
{
    public ReactionDto Reaction { get; set; }

    public int Count { get; set; }

    public bool IsSelectedByCurrentUser { get; set; }
}
