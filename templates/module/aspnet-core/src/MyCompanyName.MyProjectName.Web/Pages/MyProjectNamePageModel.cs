using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;

namespace MyCompanyName.MyProjectName.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class MyProjectNamePageModel : SmartSoftwarePageModel
{
    protected MyProjectNamePageModel()
    {
        LocalizationResourceType = typeof(MyProjectNameResource);
        ObjectMapperContext = typeof(MyProjectNameWebModule);
    }
}
