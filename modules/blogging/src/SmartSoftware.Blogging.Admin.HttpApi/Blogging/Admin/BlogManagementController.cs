using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.Application.Dtos;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Blogging.Admin.Blogs;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Blogs.Dtos;

namespace SmartSoftware.Blogging.Admin
{
    [RemoteService(Name = BloggingAdminRemoteServiceConsts.RemoteServiceName)]
    [Area(BloggingAdminRemoteServiceConsts.ModuleName)]
    [Route("api/blogging/blogs/admin")]
    public class BlogManagementController : SmartSoftwareControllerBase, IBlogManagementAppService
    {
        private readonly IBlogManagementAppService _blogManagementAppService;

        public BlogManagementController(IBlogManagementAppService blogManagementAppService)
        {
            _blogManagementAppService = blogManagementAppService;
        }

        [HttpGet]
        public virtual async Task<ListResultDto<BlogDto>> GetListAsync()
        {
            return await _blogManagementAppService.GetListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<BlogDto> GetAsync(Guid id)
        {
            return await _blogManagementAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual async Task<BlogDto> CreateAsync(CreateBlogDto input)
        {
            return await _blogManagementAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<BlogDto> UpdateAsync(Guid id, UpdateBlogDto input)
        {
            return await _blogManagementAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _blogManagementAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("clear-cache/{id}")]
        public virtual async Task ClearCacheAsync(Guid id)
        {
            await _blogManagementAppService.ClearCacheAsync(id);
        }
    }
}
