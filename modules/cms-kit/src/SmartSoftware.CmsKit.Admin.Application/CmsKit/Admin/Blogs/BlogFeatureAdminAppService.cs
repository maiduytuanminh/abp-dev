using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmartSoftware.EventBus.Distributed;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Blogs;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.Permissions;

namespace SmartSoftware.CmsKit.Admin.Blogs;

[RequiresFeature(CmsKitFeatures.BlogEnable)]
[RequiresGlobalFeature(typeof(BlogsFeature))]
[Authorize(CmsKitAdminPermissions.Blogs.Features)]
public class BlogFeatureAdminAppService : CmsKitAdminAppServiceBase, IBlogFeatureAdminAppService
{
    protected IBlogFeatureRepository BlogFeatureRepository { get; }

    protected BlogFeatureManager BlogFeatureManager { get; }

    protected IDistributedEventBus EventBus { get; }

    public BlogFeatureAdminAppService(
        IBlogFeatureRepository blogFeatureRepository,
        BlogFeatureManager blogFeatureManager,
        IDistributedEventBus eventBus)
    {
        BlogFeatureRepository = blogFeatureRepository;
        BlogFeatureManager = blogFeatureManager;
        EventBus = eventBus;
    }

    public virtual async Task<List<BlogFeatureDto>> GetListAsync(Guid blogId)
    {
        var blogFeatures = await BlogFeatureRepository.GetListAsync(blogId);

        return ObjectMapper.Map<List<BlogFeature>, List<BlogFeatureDto>>(blogFeatures);
    }

    public virtual Task SetAsync(Guid blogId, BlogFeatureInputDto dto)
    {
        return BlogFeatureManager.SetAsync(blogId, dto.FeatureName, dto.IsEnabled);
    }
}
