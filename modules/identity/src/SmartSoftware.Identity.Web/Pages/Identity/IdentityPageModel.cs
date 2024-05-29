using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;

namespace SmartSoftware.Identity.Web.Pages.Identity;

public abstract class IdentityPageModel : SmartSoftwarePageModel
{
    protected IdentityPageModel()
    {
        ObjectMapperContext = typeof(SmartSoftwareIdentityWebModule);
    }
}
