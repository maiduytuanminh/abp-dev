using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.CmsKit.Localization;

namespace SmartSoftware.CmsKit.Web.Pages;

public abstract class CommonPageModel : SmartSoftwarePageModel
{
    protected CommonPageModel()
    {
        LocalizationResourceType = typeof(CmsKitResource);
    }
}
