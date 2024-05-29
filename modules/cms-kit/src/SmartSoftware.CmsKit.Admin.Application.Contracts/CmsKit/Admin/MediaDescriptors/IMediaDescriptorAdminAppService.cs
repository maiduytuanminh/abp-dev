using System;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.CmsKit.Admin.MediaDescriptors;

public interface IMediaDescriptorAdminAppService : IApplicationService
{
    Task<MediaDescriptorDto> CreateAsync(string entityType, CreateMediaInputWithStream inputStream);

    Task DeleteAsync(Guid id);
}
