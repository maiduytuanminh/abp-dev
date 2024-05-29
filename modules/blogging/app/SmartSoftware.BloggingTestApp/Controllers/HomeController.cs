using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Blogging;

namespace SmartSoftware.BloggingTestApp.Controllers
{
    public class HomeController : SmartSoftwareController
    {
        private readonly BloggingUrlOptions _blogOptions;

        public HomeController(IOptions<BloggingUrlOptions> blogOptions)
        {
            _blogOptions = blogOptions.Value;
        }
        public ActionResult Index()
        {
            var urlPrefix = _blogOptions.RoutePrefix;
            return Redirect(urlPrefix);
        }
    }
}
