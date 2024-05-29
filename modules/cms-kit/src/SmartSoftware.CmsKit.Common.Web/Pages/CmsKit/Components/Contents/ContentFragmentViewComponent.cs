using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.Widgets;
using SmartSoftware.CmsKit.Contents;

namespace SmartSoftware.CmsKit.Web.Pages.CmsKit.Components.Contents;

[ViewComponent(Name = "ContentFragment")]
[Widget(
    AutoInitialize = true
)]
public class ContentFragmentViewComponent : SmartSoftwareViewComponent
{
    public IContent ContentDto { get; set; }

    public virtual IViewComponentResult Invoke(IContent contentDto)
    {
        return View("~/Pages/CmsKit/Components/Contents/ContentFragment.cshtml", new ContentFragmentViewComponent() { ContentDto = contentDto });
    }
}