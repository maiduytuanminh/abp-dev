using System;
using System.ComponentModel.DataAnnotations;

namespace SmartSoftware.CmsKit.Admin.Blogs;

[Serializable]
public class BlogFeatureInputDto
{
    [Required]
    public string FeatureName { get; set; }

    public bool IsEnabled { get; set; }
}
