using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;
using SmartSoftware.CmsKit.Blogs;

namespace SmartSoftware.CmsKit.Admin.Blogs;

public interface IBlogFeatureAdminAppService : IApplicationService
{
    Task SetAsync(Guid blogId, BlogFeatureInputDto dto);

    Task<List<BlogFeatureDto>> GetListAsync(Guid blogId);
}
