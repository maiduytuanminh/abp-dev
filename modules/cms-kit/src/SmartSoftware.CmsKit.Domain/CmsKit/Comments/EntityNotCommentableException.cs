using SmartSoftware;

namespace SmartSoftware.CmsKit.Comments;

public class EntityNotCommentableException : BusinessException
{
    public EntityNotCommentableException(string entityType)
    {
        Code = CmsKitErrorCodes.Comments.EntityNotCommentable;
        EntityType = entityType;
        WithData(nameof(EntityType), EntityType);
    }

    public string EntityType { get; }
}
