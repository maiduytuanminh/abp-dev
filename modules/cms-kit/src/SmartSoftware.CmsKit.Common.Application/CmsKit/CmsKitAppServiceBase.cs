using SmartSoftware.Application.Services;
using SmartSoftware.CmsKit.Localization;

namespace SmartSoftware.CmsKit;

public abstract class CmsKitAppServiceBase : ApplicationService
{
    protected CmsKitAppServiceBase()
    {
        LocalizationResource = typeof(CmsKitResource);
    }
}
