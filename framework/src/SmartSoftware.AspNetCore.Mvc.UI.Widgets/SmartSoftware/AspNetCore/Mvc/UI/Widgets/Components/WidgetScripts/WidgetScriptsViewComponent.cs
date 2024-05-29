using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc.UI.Widgets.Components.WidgetStyles;

namespace SmartSoftware.AspNetCore.Mvc.UI.Widgets.Components.WidgetScripts;

public class WidgetScriptsViewComponent : SmartSoftwareViewComponent
{
    protected IPageWidgetManager PageWidgetManager { get; }
    protected SmartSoftwareWidgetOptions Options { get; }

    public WidgetScriptsViewComponent(
        IPageWidgetManager pageWidgetManager,
        IOptions<SmartSoftwareWidgetOptions> options)
    {
        PageWidgetManager = pageWidgetManager;
        Options = options.Value;
    }

    public virtual IViewComponentResult Invoke()
    {
        return View(
            "~/SmartSoftware/AspNetCore/Mvc/UI/Widgets/Components/WidgetScripts/Default.cshtml",
            new WidgetResourcesViewModel
            {
                Widgets = PageWidgetManager.GetAll()
            }
        );
    }
}
