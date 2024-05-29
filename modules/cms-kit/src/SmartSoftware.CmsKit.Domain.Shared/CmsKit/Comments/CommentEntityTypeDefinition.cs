using JetBrains.Annotations;
using SmartSoftware;

namespace SmartSoftware.CmsKit.Comments;

public class CommentEntityTypeDefinition : EntityTypeDefinition
{
    public CommentEntityTypeDefinition([NotNull] string entityType) : base(entityType)
    {
        EntityType = Check.NotNullOrEmpty(entityType, nameof(entityType));
    }
}
