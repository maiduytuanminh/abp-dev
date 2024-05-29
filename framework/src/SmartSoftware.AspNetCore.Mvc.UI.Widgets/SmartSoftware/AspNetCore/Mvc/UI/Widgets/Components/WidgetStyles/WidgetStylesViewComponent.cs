using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SmartSoftware.AspNetCore.Mvc.UI.Widgets.Components.WidgetStyles;

public class WidgetStylesViewComponent : SmartSoftwareViewComponent
{
    protected IPageWidgetManager PageWidgetManager { get; }
    protected SmartSoftwareWidgetOptions Options { get; }

    public WidgetStylesViewComponent(
        IPageWidgetManager pageWidgetManager,
        IOptions<SmartSoftwareWidgetOptions> options)
    {
        PageWidgetManager = pageWidgetManager;
        Options = options.Value;
    }

    public virtual IViewComponentResult Invoke()
    {
        return View(
            "~/SmartSoftware/AspNetCore/Mvc/UI/Widgets/Components/WidgetStyles/Default.cshtml",
            new WidgetResourcesViewModel
            {
                Widgets = PageWidgetManager.GetAll()
            }
        );
    }
}
