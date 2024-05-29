using System;
using Microsoft.AspNetCore.Mvc;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Pages.Shared.Components.SmartSoftwareApplicationPath;

public class SmartSoftwareApplicationPathViewComponent : SmartSoftwareViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        var applicationPath = ViewContext.HttpContext.Request.PathBase.Value;
        var model = new SmartSoftwareApplicationPathViewComponentModel
        {
            ApplicationPath = applicationPath == null ? "/" : applicationPath.EnsureEndsWith('/')
        };

        return View("~/Pages/Shared/Components/SmartSoftwareApplicationPath/Default.cshtml", model);
    }
}
