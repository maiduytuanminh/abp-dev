using SmartSoftware.Application.Services;
using SmartSoftware.Blogging.Localization;

namespace SmartSoftware.Blogging
{
    public abstract class BloggingAppServiceBase : ApplicationService
    {
        protected BloggingAppServiceBase()
        {
            ObjectMapperContext = typeof(BloggingApplicationModule);
            LocalizationResource = typeof(BloggingResource);
        }
    }
}