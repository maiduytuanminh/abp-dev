using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SmartSoftware;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Blogs;
using SmartSoftware.CmsKit.GlobalFeatures;

namespace SmartSoftware.CmsKit.Blogs;

[RequiresGlobalFeature(typeof(BlogsFeature))]
[RemoteService(Name = CmsKitCommonRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitCommonRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit/blogs/{blogId}/features")]
public class BlogFeatureController : CmsKitControllerBase, IBlogFeatureAppService
{
    protected IBlogFeatureAppService BlogFeatureAppService { get; }

    public BlogFeatureController(IBlogFeatureAppService blogFeatureAppService)
    {
        BlogFeatureAppService = blogFeatureAppService;
    }

    [HttpGet]
    [Route("{featureName}")]
    public Task<BlogFeatureDto> GetOrDefaultAsync(Guid blogId, string featureName)
    {
        return BlogFeatureAppService.GetOrDefaultAsync(blogId, featureName);
    }
}
