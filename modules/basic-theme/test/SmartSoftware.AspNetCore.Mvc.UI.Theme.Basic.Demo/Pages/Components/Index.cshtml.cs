using System.Linq;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic.Demo.Menus;
using SmartSoftware.UI.Navigation;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic.Demo.Pages.Components;

public class IndexModel : SmartSoftwarePageModel
{
    public readonly IMenuManager _menuManager;

    public IndexModel(IMenuManager menuManager)
    {
        _menuManager = menuManager;
    }

    public void OnGet()
    {

    }
}
