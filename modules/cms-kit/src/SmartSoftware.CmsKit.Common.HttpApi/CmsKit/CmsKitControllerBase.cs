using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.CmsKit.Localization;

namespace SmartSoftware.CmsKit;

public abstract class CmsKitControllerBase : SmartSoftwareControllerBase
{
    protected CmsKitControllerBase()
    {
        LocalizationResource = typeof(CmsKitResource);
    }
}
