using System.Collections.Generic;

namespace SmartSoftware.AspNetCore.Mvc.UI.Widgets;

public interface IPageWidgetManager
{
    bool TryAdd(WidgetDefinition widget);

    IReadOnlyList<WidgetDefinition> GetAll();
}
