using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;

namespace MyCompanyName.MyProjectName.Controllers;

public class HomeController : SmartSoftwareController
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}
