﻿using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Prismjs;
using SmartSoftware.AspNetCore.Mvc.UI.Widgets;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Demo.Views.Components.Themes.Shared.Demos.CarouselDemo;

[Widget(
    StyleTypes = new[] { typeof(PrismjsStyleBundleContributor) },
    ScriptTypes = new[] { typeof(PrismjsScriptBundleContributor) }
)]
public class CarouselDemoViewComponent : SmartSoftwareViewComponent
{
    public const string ViewPath = "/Views/Components/Themes/Shared/Demos/CarouselDemo/Default.cshtml";

    public virtual IViewComponentResult Invoke()
    {
        return View(ViewPath);
    }
}
