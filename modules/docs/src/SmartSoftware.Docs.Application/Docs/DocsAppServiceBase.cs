using SmartSoftware.Application.Services;
using SmartSoftware.Docs.Localization;

namespace SmartSoftware.Docs
{
    public abstract class DocsAppServiceBase : ApplicationService
    {
        protected DocsAppServiceBase()
        {
            ObjectMapperContext = typeof(DocsApplicationModule);
            LocalizationResource = typeof(DocsResource);
        }
    }
}