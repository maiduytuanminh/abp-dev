using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.Tags;

namespace SmartSoftware.CmsKit.Public.Tags;

[RequiresFeature(CmsKitFeatures.TagEnable)]
[RequiresGlobalFeature(typeof(TagsFeature))]
[RemoteService(Name = CmsKitPublicRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/tags")]
public class TagPublicController : CmsKitPublicControllerBase, ITagAppService
{
    protected ITagAppService TagAppService { get; }

    public TagPublicController(ITagAppService tagAppService)
    {
        TagAppService = tagAppService;
    }

    [HttpGet]
    [Route("{entityType}/{entityId}")]
    public Task<List<TagDto>> GetAllRelatedTagsAsync(string entityType, string entityId)
    {
        return TagAppService.GetAllRelatedTagsAsync(entityType, entityId);
    }

    [HttpGet]
    [Route("popular/{entityType}/{maxCount:int}")]
    public Task<List<PopularTagDto>> GetPopularTagsAsync(string entityType, int maxCount)
    {
        return TagAppService.GetPopularTagsAsync(entityType, maxCount);
    }
}
