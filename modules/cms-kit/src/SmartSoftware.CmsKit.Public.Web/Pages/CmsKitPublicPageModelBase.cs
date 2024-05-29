using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.CmsKit.Localization;

namespace SmartSoftware.CmsKit.Public.Web.Pages;

public abstract class CmsKitPublicPageModelBase : SmartSoftwarePageModel
{
    protected CmsKitPublicPageModelBase()
    {
        LocalizationResourceType = typeof(CmsKitResource);
        ObjectMapperContext = typeof(CmsKitPublicWebModule);
    }
}
