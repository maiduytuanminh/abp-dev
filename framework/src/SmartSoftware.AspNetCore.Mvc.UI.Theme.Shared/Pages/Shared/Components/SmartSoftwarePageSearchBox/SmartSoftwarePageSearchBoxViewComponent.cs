using Microsoft.AspNetCore.Mvc;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Pages.Shared.Components.SmartSoftwarePageSearchBox;

public class SmartSoftwarePageSearchBoxViewComponent : SmartSoftwareViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View("~/Pages/Shared/Components/SmartSoftwarePageSearchBox/Default.cshtml");
    }
}
