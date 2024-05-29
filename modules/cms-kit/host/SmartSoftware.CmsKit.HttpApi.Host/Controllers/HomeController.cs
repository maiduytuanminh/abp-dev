using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;

namespace SmartSoftware.CmsKit.Controllers;

public class HomeController : SmartSoftwareController
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}
