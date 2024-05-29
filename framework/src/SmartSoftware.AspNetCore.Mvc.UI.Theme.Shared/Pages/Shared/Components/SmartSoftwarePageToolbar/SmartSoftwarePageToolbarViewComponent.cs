using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Pages.Shared.Components.SmartSoftwarePageToolbar;

public class SmartSoftwarePageToolbarViewComponent : SmartSoftwareViewComponent
{
    protected IPageToolbarManager ToolbarManager { get; }

    public SmartSoftwarePageToolbarViewComponent(IPageToolbarManager toolbarManager)
    {
        ToolbarManager = toolbarManager;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync(string pageName)
    {
        var items = await ToolbarManager.GetItemsAsync(pageName);
        return View("~/Pages/Shared/Components/SmartSoftwarePageToolbar/Default.cshtml", items);
    }
}
