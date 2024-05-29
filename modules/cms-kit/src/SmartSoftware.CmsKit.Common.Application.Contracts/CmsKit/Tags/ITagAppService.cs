using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.CmsKit.Tags;

public interface ITagAppService : IApplicationService
{
    Task<List<TagDto>> GetAllRelatedTagsAsync(string entityType, string entityId);
    Task<List<PopularTagDto>> GetPopularTagsAsync(string entityType, int maxCount);
}