using Microsoft.AspNetCore.Mvc;

namespace SmartSoftwarePerfTest.WithSmartSoftware.Controllers
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
