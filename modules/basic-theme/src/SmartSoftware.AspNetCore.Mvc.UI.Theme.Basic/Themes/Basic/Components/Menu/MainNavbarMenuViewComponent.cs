using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.UI.Navigation;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic.Themes.Basic.Components.Menu;

public class MainNavbarMenuViewComponent : SmartSoftwareViewComponent
{
    protected IMenuManager MenuManager { get; }

    public MainNavbarMenuViewComponent(IMenuManager menuManager)
    {
        MenuManager = menuManager;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var menu = await MenuManager.GetMainMenuAsync();
        return View("~/Themes/Basic/Components/Menu/Default.cshtml", menu);
    }
}
