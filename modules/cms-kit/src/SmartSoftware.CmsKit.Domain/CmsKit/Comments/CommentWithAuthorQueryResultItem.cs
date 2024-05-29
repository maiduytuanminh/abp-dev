using SmartSoftware.CmsKit.Users;

namespace SmartSoftware.CmsKit.Comments;

public class CommentWithAuthorQueryResultItem
{
    public Comment Comment { get; set; }

    public CmsUser Author { get; set; }
}
