using System;
using System.Collections.Generic;

namespace SmartSoftware.CmsKit.Public.Blogs;

[Serializable]
public class GetBlogFeatureInput
{
    public List<string> FeatureNames { get; set; }
}
