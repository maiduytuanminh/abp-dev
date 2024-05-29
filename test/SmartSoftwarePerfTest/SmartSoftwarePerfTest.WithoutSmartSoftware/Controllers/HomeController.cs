using Microsoft.AspNetCore.Mvc;

namespace SmartSoftwarePerfTest.WithoutSmartSoftware.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Redirect("/api/books/");
        }
    }
}
