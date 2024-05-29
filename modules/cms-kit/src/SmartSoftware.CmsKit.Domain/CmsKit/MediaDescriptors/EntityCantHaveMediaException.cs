using SmartSoftware;

namespace SmartSoftware.CmsKit.MediaDescriptors;

public class EntityCantHaveMediaException : BusinessException
{
    public EntityCantHaveMediaException(string entityType)
      : base(code: CmsKitErrorCodes.MediaDescriptors.EntityTypeDoesntExist)
    {
        EntityType = entityType;
        WithData(nameof(entityType), EntityType);
    }

    public string EntityType { get; }
}
