using SmartSoftware.CmsKit.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;

namespace SmartSoftware.CmsKit.Pages;

public abstract class CmsKitPageModel : SmartSoftwarePageModel
{
    protected CmsKitPageModel()
    {
        LocalizationResourceType = typeof(CmsKitResource);
    }
}
