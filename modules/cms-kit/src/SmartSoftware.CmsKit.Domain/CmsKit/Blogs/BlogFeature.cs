using JetBrains.Annotations;
using System;
using SmartSoftware;
using SmartSoftware.Domain.Entities.Auditing;

namespace SmartSoftware.CmsKit.Blogs;

public class BlogFeature : FullAuditedAggregateRoot<Guid>
{
    public Guid BlogId { get; protected set; }

    public string FeatureName { get; protected set; }

    public bool IsEnabled { get; protected internal set; }

    protected BlogFeature()
    {
    }

    public BlogFeature(Guid blogId, [NotNull] string featureName, bool isEnabled = true)
    {
        BlogId = blogId;
        FeatureName = Check.NotNullOrWhiteSpace(featureName, nameof(featureName));
        IsEnabled = isEnabled;
    }
}
