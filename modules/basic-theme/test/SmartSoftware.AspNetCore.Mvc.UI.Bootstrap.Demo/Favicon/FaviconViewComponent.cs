using Microsoft.AspNetCore.Mvc;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Demo.Favicon;

public class FaviconViewComponent : SmartSoftwareViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("~/Favicon/Default.cshtml");
    }
}