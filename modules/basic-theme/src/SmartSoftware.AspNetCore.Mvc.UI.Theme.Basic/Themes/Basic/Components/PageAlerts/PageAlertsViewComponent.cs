using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.Alerts;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic.Themes.Basic.Components.PageAlerts;

public class PageAlertsViewComponent : SmartSoftwareViewComponent
{
    protected IAlertManager AlertManager { get; }

    public PageAlertsViewComponent(IAlertManager alertManager)
    {
        AlertManager = alertManager;
    }

    public IViewComponentResult Invoke(string name)
    {
        return View("~/Themes/Basic/Components/PageAlerts/Default.cshtml", AlertManager.Alerts);
    }
}
