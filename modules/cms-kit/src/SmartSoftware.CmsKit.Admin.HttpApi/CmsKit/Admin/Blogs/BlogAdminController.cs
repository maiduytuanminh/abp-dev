using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.Permissions;

namespace SmartSoftware.CmsKit.Admin.Blogs;

[RequiresFeature(CmsKitFeatures.BlogEnable)]
[RequiresGlobalFeature(typeof(BlogsFeature))]
[RemoteService(Name = CmsKitAdminRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitAdminRemoteServiceConsts.ModuleName)]
[Authorize(CmsKitAdminPermissions.Blogs.Default)]
[Route("api/cms-kit-admin/blogs")]
public class BlogAdminController : CmsKitAdminController, IBlogAdminAppService
{
    protected IBlogAdminAppService BlogAdminAppService { get; }

    public BlogAdminController(IBlogAdminAppService blogAdminAppService)
    {
        BlogAdminAppService = blogAdminAppService;
    }

    [HttpGet]
    [Route("{id}")]
    public Task<BlogDto> GetAsync(Guid id)
    {
        return BlogAdminAppService.GetAsync(id);
    }

    [HttpGet]
    public Task<PagedResultDto<BlogDto>> GetListAsync(BlogGetListInput input)
    {
        return BlogAdminAppService.GetListAsync(input);
    }

    [HttpPost]
    [Authorize(CmsKitAdminPermissions.Blogs.Create)]
    public Task<BlogDto> CreateAsync(CreateBlogDto input)
    {
        return BlogAdminAppService.CreateAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(CmsKitAdminPermissions.Blogs.Update)]
    public Task<BlogDto> UpdateAsync(Guid id, UpdateBlogDto input)
    {
        return BlogAdminAppService.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(CmsKitAdminPermissions.Blogs.Delete)]
    public Task DeleteAsync(Guid id)
    {
        return BlogAdminAppService.DeleteAsync(id);
    }
}
