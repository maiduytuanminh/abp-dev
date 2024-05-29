using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Contents;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.Users;

namespace SmartSoftware.CmsKit.Public.Blogs;

[RequiresFeature(CmsKitFeatures.BlogEnable)]
[RequiresGlobalFeature(typeof(BlogsFeature))]
[RemoteService(Name = CmsKitPublicRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/blog-posts")]
public class BlogPostPublicController : CmsKitPublicControllerBase, IBlogPostPublicAppService
{
    protected IBlogPostPublicAppService BlogPostPublicAppService { get; }

    public BlogPostPublicController(IBlogPostPublicAppService blogPostPublicAppService)
    {
        BlogPostPublicAppService = blogPostPublicAppService;
    }

    [HttpGet]
    [Route("{blogSlug}/{blogPostSlug}")]
    public virtual Task<BlogPostCommonDto> GetAsync(string blogSlug, string blogPostSlug)
    {
        return BlogPostPublicAppService.GetAsync(blogSlug, blogPostSlug);
    }

    [HttpGet]
    [Route("{blogSlug}")]
    public virtual Task<PagedResultDto<BlogPostCommonDto>> GetListAsync(string blogSlug, BlogPostGetListInput input)
    {
        return BlogPostPublicAppService.GetListAsync(blogSlug, input);
    }

    [HttpGet]
    [Route("authors")]
    public virtual Task<PagedResultDto<CmsUserDto>> GetAuthorsHasBlogPostsAsync(BlogPostFilteredPagedAndSortedResultRequestDto input)
    {
        return BlogPostPublicAppService.GetAuthorsHasBlogPostsAsync(input);
    }

    [HttpGet]
    [Route("authors/{id}")]
    public virtual Task<CmsUserDto> GetAuthorHasBlogPostAsync(Guid id)
    {
        return BlogPostPublicAppService.GetAuthorHasBlogPostAsync(id);
    }

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return BlogPostPublicAppService.DeleteAsync(id);
    }

    [HttpGet]
    [Route("tags/{id}")]
    public Task<string> GetTagNameAsync([NotNull] Guid tagId)
    {
        return BlogPostPublicAppService.GetTagNameAsync(tagId);
    }
}