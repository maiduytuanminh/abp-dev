﻿using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using SmartSoftware.Caching;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.GlobalFeatures;

namespace SmartSoftware.CmsKit.Blogs;

[RequiresGlobalFeature(typeof(BlogsFeature))]
public class BlogFeatureAppService : CmsKitAppServiceBase, IBlogFeatureAppService
{
    protected virtual IBlogFeatureRepository BlogFeatureRepository { get; }

    protected virtual IDistributedCache<BlogFeatureCacheItem, BlogFeatureCacheKey> Cache { get; }

    public BlogFeatureAppService(
        IBlogFeatureRepository blogFeatureRepository,
        IDistributedCache<BlogFeatureCacheItem, BlogFeatureCacheKey> blogFeatureCacheManager)
    {
        BlogFeatureRepository = blogFeatureRepository;
        Cache = blogFeatureCacheManager;
    }

    public virtual async Task<BlogFeatureDto> GetOrDefaultAsync(Guid blogId, string featureName)
    {
        var cacheItem = await Cache.GetOrAddAsync(
                                new BlogFeatureCacheKey(blogId, featureName),
                                () => GetOrDefaultFromRepositoryAsync(blogId, featureName));

        return ObjectMapper.Map<BlogFeatureCacheItem, BlogFeatureDto>(cacheItem);
    }

    protected virtual async Task<BlogFeatureCacheItem> GetOrDefaultFromRepositoryAsync(Guid blogId, string featureName)
    {
        var feature = await BlogFeatureRepository.FindAsync(blogId, featureName);
        var blogFeature = feature ?? new BlogFeature(blogId, featureName);

        return ObjectMapper.Map<BlogFeature, BlogFeatureCacheItem>(blogFeature);
    }
}
