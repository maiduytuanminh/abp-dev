using System;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.CmsKit.Blogs;

public interface IBlogFeatureAppService : IApplicationService
{
    Task<BlogFeatureDto> GetOrDefaultAsync(Guid blogId, string featureName);
}
