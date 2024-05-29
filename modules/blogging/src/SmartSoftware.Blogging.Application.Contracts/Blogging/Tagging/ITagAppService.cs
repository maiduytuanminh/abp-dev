using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;
using SmartSoftware.Blogging.Tagging.Dtos;

namespace SmartSoftware.Blogging.Tagging
{
    public interface ITagAppService : IApplicationService
    {
        Task<List<TagDto>> GetPopularTagsAsync(Guid blogId, GetPopularTagsInput input);

    }
}
