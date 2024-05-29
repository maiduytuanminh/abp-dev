using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSoftware.Ui.LayoutHooks;

namespace SmartSoftware.AspNetCore.Mvc.UI.Components.LayoutHook;

public class LayoutHookViewComponent : SmartSoftwareViewComponent
{
    protected SmartSoftwareLayoutHookOptions Options { get; }

    public LayoutHookViewComponent(IOptions<SmartSoftwareLayoutHookOptions> options)
    {
        Options = options.Value;
    }

    public virtual IViewComponentResult Invoke(string name, string? layout)
    {
        var hooks = Options.Hooks.GetOrDefault(name)?
            .Where(x => IsViewComponent(x) && (string.IsNullOrWhiteSpace(x.Layout) || x.Layout == layout))
            .ToArray() ?? Array.Empty<LayoutHookInfo>();

        return View(
            "~/SmartSoftware/AspNetCore/Mvc/UI/Components/LayoutHook/Default.cshtml",
            new LayoutHookViewModel(hooks, layout)
        );
    }

    protected virtual bool IsViewComponent(LayoutHookInfo layoutHook)
    {
        return typeof(ViewComponent).IsAssignableFrom(layoutHook.ComponentType);
    }
}
