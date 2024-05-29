using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.CmsKit.Web.Pages.CmsKit.Components.ContentPreview;

namespace SmartSoftware.CmsKit.Web.Controllers;

public class CmsKitCommonWidgetsController : SmartSoftwareController
{
    [HttpPost]
    public IActionResult ContentPreview(ContentPreviewDto dto)
    {
        return ViewComponent(typeof(ContentPreviewViewComponent), new { content = dto.Content });
    }
}
