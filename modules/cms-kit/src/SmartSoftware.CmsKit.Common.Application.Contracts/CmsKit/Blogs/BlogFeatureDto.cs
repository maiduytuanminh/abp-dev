using System;
using SmartSoftware.Application.Dtos;

namespace SmartSoftware.CmsKit.Blogs;

[Serializable]
public class BlogFeatureDto : ExtensibleEntityDto<Guid>
{
    public string FeatureName { get; set; }
    public bool IsEnabled { get; set; }
}
