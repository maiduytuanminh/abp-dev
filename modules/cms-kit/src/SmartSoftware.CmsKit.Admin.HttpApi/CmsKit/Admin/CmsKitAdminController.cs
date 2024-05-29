using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.CmsKit.Localization;

namespace SmartSoftware.CmsKit.Admin;

public abstract class CmsKitAdminController : SmartSoftwareControllerBase
{
    protected CmsKitAdminController()
    {
        LocalizationResource = typeof(CmsKitResource);
    }
}
