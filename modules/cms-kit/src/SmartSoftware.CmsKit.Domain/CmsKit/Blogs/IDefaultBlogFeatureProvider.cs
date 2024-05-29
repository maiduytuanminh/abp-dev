using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSoftware.CmsKit.Blogs;

namespace SmartSoftware.CmsKit.Blogs;

public interface IDefaultBlogFeatureProvider
{
    Task<List<BlogFeature>> GetDefaultFeaturesAsync(Guid blogId);
}
