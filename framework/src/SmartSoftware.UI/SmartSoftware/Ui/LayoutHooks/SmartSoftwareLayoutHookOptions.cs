using System;
using System.Collections.Generic;

namespace SmartSoftware.Ui.LayoutHooks;

public class SmartSoftwareLayoutHookOptions
{
    public IDictionary<string, List<LayoutHookInfo>> Hooks { get; }

    public SmartSoftwareLayoutHookOptions()
    {
        Hooks = new Dictionary<string, List<LayoutHookInfo>>();
    }

    public SmartSoftwareLayoutHookOptions Add(string name, Type componentType, string? layout = null)
    {
        Hooks
            .GetOrAdd(name, () => new List<LayoutHookInfo>())
            .Add(new LayoutHookInfo(componentType, layout));

        return this;
    }
}
