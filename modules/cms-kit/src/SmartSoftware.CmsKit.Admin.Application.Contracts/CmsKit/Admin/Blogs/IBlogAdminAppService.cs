using System;
using SmartSoftware.Application.Services;

namespace SmartSoftware.CmsKit.Admin.Blogs;

public interface IBlogAdminAppService : ICrudAppService<BlogDto, Guid, BlogGetListInput, CreateBlogDto, UpdateBlogDto>
{
}
