using System.Collections.Generic;

namespace SmartSoftware.AspNetCore.Mvc.UI.Widgets.Components;

public class WidgetResourcesViewModel
{
    public IReadOnlyList<WidgetDefinition> Widgets { get; set; } = default!;

}
