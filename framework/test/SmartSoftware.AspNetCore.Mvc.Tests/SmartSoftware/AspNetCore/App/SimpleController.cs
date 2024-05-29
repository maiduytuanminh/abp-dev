using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;

namespace SmartSoftware.AspNetCore.App;

public class SimpleController : SmartSoftwareController
{
    public ActionResult Index()
    {
        return Content("Index-Result");
    }

    public ActionResult About()
    {
        // ReSharper disable once Mvc.ViewNotResolved
        return View();
    }

    public ActionResult ExceptionOnRazor()
    {
        // ReSharper disable once Mvc.ViewNotResolved
        return View();
    }
}
