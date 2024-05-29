using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Prismjs;
using SmartSoftware.AspNetCore.Mvc.UI.Widgets;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Demo.Views.Components.Themes.Shared.Demos.ListGroupsDemo;

[Widget(
    StyleTypes = new[] { typeof(PrismjsStyleBundleContributor) },
    ScriptTypes = new[] { typeof(PrismjsScriptBundleContributor) }
)]
public class ListGroupsDemoViewComponent : SmartSoftwareViewComponent
{
    public const string ViewPath = "/Views/Components/Themes/Shared/Demos/ListGroupsDemo/Default.cshtml";

    public virtual IViewComponentResult Invoke()
    {
        return View(ViewPath);
    }
}
