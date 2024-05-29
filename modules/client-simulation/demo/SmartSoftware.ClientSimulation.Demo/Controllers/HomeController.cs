using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;

namespace SmartSoftware.ClientSimulation.Demo.Controllers;

public class HomeController : SmartSoftwareController
{
    public ActionResult Index()
    {
        return Redirect("/ClientSimulation");
    }
}
