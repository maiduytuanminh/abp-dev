using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.Widgets;

namespace SmartSoftware.CmsKit.ViewComponents;

[Widget(
    AutoInitialize = true
)]

[ViewComponent(Name = "CmsToday")]
public class TodayViewComponent : SmartSoftwareViewComponent
{
    public string Format { get; set; }

    public TodayViewComponent()
    {
    }

    public virtual async Task<IViewComponentResult> InvokeAsync(string format)
    {
        return View("~/ViewComponents/Today.cshtml", new TodayViewComponent() { Format = format });
    }
}
