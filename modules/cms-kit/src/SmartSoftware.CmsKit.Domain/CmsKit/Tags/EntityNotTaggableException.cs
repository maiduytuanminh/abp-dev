using SmartSoftware;

namespace SmartSoftware.CmsKit.Tags;

public class EntityNotTaggableException : BusinessException
{
    public EntityNotTaggableException(string entityType)
    {
        Code = CmsKitErrorCodes.Tags.EntityNotTaggable;
        WithData(nameof(Tag.EntityType), entityType);
    }
}
