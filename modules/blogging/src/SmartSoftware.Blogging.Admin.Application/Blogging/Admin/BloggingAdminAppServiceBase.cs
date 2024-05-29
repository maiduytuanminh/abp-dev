using SmartSoftware.Application.Services;
using SmartSoftware.Blogging.Localization;

namespace SmartSoftware.Blogging.Admin
{
    public abstract class BloggingAdminAppServiceBase : ApplicationService
    {
        protected BloggingAdminAppServiceBase()
        {
            ObjectMapperContext = typeof(BloggingAdminApplicationModule);
            LocalizationResource = typeof(BloggingResource);
        }
    }
}
