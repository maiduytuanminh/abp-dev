using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;

namespace MyCompanyName.MyProjectName.Blazor.Server.Tiered.Components.Toolbar.LoginLink;

public class LoginLinkViewComponent : SmartSoftwareViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View("~/Components/Toolbar/LoginLink/Default.cshtml");
    }
}
