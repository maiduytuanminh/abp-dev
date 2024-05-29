using System;
using System.Threading.Tasks;
using SmartSoftware.BlobStoring;
using SmartSoftware.Content;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.GlobalFeatures;

namespace SmartSoftware.CmsKit.MediaDescriptors;

[RequiresGlobalFeature(typeof(MediaFeature))]
public class MediaDescriptorAppService : CmsKitAppServiceBase, IMediaDescriptorAppService
{
    protected IMediaDescriptorRepository MediaDescriptorRepository { get; }
    protected IBlobContainer<MediaContainer> MediaContainer { get; }

    public MediaDescriptorAppService(IMediaDescriptorRepository mediaDescriptorRepository, IBlobContainer<MediaContainer> mediaContainer)
    {
        MediaDescriptorRepository = mediaDescriptorRepository;
        MediaContainer = mediaContainer;
    }

    public virtual async Task<RemoteStreamContent> DownloadAsync(Guid id)
    {
        var entity = await MediaDescriptorRepository.GetAsync(id);
        var stream = await MediaContainer.GetAsync(id.ToString());

        return new RemoteStreamContent(stream, entity.Name, entity.MimeType);
    }
}
