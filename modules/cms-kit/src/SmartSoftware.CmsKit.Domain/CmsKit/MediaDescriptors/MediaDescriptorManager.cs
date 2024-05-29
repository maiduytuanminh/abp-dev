using System.Threading.Tasks;
using SmartSoftware.Domain.Services;

namespace SmartSoftware.CmsKit.MediaDescriptors;

public class MediaDescriptorManager : DomainService
{
    protected IMediaDescriptorDefinitionStore MediaDescriptorDefinitionStore { get; }

    public MediaDescriptorManager(IMediaDescriptorDefinitionStore mediaDescriptorDefinitionStore)
    {
        MediaDescriptorDefinitionStore = mediaDescriptorDefinitionStore;
    }

    public virtual async Task<MediaDescriptor> CreateAsync(string entityType, string name, string mimeType, long size)
    {
        if (!await MediaDescriptorDefinitionStore.IsDefinedAsync(entityType))
        {
            throw new EntityCantHaveMediaException(entityType);
        }

        return new MediaDescriptor(
            GuidGenerator.Create(),
            entityType,
            name,
            mimeType,
            size,
            CurrentTenant.Id);
    }
}
