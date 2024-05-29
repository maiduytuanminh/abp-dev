using JetBrains.Annotations;
using SmartSoftware.CmsKit.Web.Reactions;

namespace SmartSoftware.CmsKit.Web;

public class CmsKitUiOptions
{
    [NotNull]
    public ReactionIconDictionary ReactionIcons { get; }

    public CmsKitUiCommentOptions CommentsOptions { get; }

    public CmsKitUiOptions()
    {
        ReactionIcons = new ReactionIconDictionary();
        CommentsOptions = new CmsKitUiCommentOptions();
    }
}
