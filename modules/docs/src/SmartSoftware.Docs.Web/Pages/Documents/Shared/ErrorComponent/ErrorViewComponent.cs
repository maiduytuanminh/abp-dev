using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc;

namespace SmartSoftware.Docs.Pages.Documents.Shared.ErrorComponent
{
    public class ErrorViewComponent : SmartSoftwareViewComponent
    {
        public IViewComponentResult Invoke(ErrorPageModel model)
        {
            return View("~/Pages/Documents/Shared/ErrorComponent/Default.cshtml", model);
        }
    }
}
