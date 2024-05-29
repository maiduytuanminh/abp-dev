using System;
using System.Threading.Tasks;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Application.Services;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Blogs.Dtos;

namespace SmartSoftware.Blogging.Admin.Blogs
{
    public interface IBlogManagementAppService : IApplicationService
    {
        Task<ListResultDto<BlogDto>> GetListAsync();

        Task<BlogDto> GetAsync(Guid id);

        Task<BlogDto> CreateAsync(CreateBlogDto input);

        Task<BlogDto> UpdateAsync(Guid id, UpdateBlogDto input);

        Task DeleteAsync(Guid id);

        Task ClearCacheAsync(Guid id);
    }
}
