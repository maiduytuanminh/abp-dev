using System;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;
using SmartSoftware.Content;

namespace SmartSoftware.CmsKit.MediaDescriptors;

public interface IMediaDescriptorAppService : IApplicationService
{
    Task<RemoteStreamContent> DownloadAsync(Guid id);
}
