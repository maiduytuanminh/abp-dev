using System;
using SmartSoftware.ObjectExtending;

namespace SmartSoftware.CmsKit.Blogs;

[Serializable]
public class BlogFeatureCacheItem : ExtensibleObject
{
    public Guid Id { get; set; }
    public string FeatureName { get; set; }
    public bool IsEnabled { get; set; }
}
