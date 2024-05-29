using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;

namespace SmartSoftware.Account.Web.Modules.Account.Components.Toolbar.UserLoginLink;

public class UserLoginLinkViewComponent : SmartSoftwareViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View("~/Modules/Account/Components/Toolbar/UserLoginLink/Default.cshtml");
    }
}
