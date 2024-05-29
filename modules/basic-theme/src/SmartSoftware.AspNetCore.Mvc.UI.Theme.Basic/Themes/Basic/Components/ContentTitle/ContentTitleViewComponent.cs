using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.Layout;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic.Themes.Basic.Components.ContentTitle;

public class ContentTitleViewComponent : SmartSoftwareViewComponent
{
    protected IPageLayout PageLayout { get; }

    public ContentTitleViewComponent(IPageLayout pageLayout)
    {
        PageLayout = pageLayout;
    }

    public virtual IViewComponentResult Invoke()
    {
        return View("~/Themes/Basic/Components/ContentTitle/Default.cshtml", PageLayout.Content);
    }
}
