using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Blogging.Localization;

namespace SmartSoftware.Blogging.Areas.Blog.Controllers
{
    public abstract class BloggingControllerBase : SmartSoftwareController
    {
        protected BloggingControllerBase()
        {
            ObjectMapperContext = typeof(BloggingWebModule);
            LocalizationResource = typeof(BloggingResource);
        }
    }
}