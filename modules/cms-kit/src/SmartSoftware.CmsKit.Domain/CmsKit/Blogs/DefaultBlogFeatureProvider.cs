using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;
using SmartSoftware.CmsKit.GlobalFeatures;

namespace SmartSoftware.CmsKit.Blogs;

public class DefaultBlogFeatureProvider : IDefaultBlogFeatureProvider, ITransientDependency
{
    public virtual Task<List<BlogFeature>> GetDefaultFeaturesAsync(Guid blogId)
    {
        return Task.FromResult(new List<BlogFeature>
        {
            new BlogFeature(blogId, CommentsFeature.Name),
            new BlogFeature(blogId, ReactionsFeature.Name),
            new BlogFeature(blogId, RatingsFeature.Name),
            new BlogFeature(blogId, TagsFeature.Name),
            new BlogFeature(blogId, BlogPostScrollIndexFeature.Name),
            new BlogFeature(blogId, BlogConsts.PreventXssFeatureName)
        });
    }
}
