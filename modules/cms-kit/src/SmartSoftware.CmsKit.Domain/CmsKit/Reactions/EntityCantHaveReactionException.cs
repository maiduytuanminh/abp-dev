using JetBrains.Annotations;
using SmartSoftware;

namespace SmartSoftware.CmsKit.Reactions;

public class EntityCantHaveReactionException : BusinessException
{
    public EntityCantHaveReactionException([NotNull] string entityType)
    {
        EntityType = Check.NotNullOrEmpty(entityType, nameof(entityType));
        Code = CmsKitErrorCodes.Reactions.EntityCantHaveReaction;
        WithData(nameof(EntityType), EntityType);
    }

    public string EntityType { get; }
}
