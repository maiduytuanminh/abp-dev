using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.CmsKit.Localization;

namespace SmartSoftware.CmsKit.Admin.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class CmsKitAdminPageModel : SmartSoftwarePageModel
{
    protected CmsKitAdminPageModel()
    {
        LocalizationResourceType = typeof(CmsKitResource);
        ObjectMapperContext = typeof(CmsKitAdminWebModule);
    }
}
