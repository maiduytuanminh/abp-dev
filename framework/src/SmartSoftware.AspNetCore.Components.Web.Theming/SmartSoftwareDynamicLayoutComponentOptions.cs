using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SmartSoftware.AspNetCore.Components.Web.Theming;

public class SmartSoftwareDynamicLayoutComponentOptions
{
    /// <summary>
    /// Used to define components that renders in the layout
    /// </summary>
    [NotNull]
    public Dictionary<Type, IDictionary<string,object>?> Components { get; set; }

    public SmartSoftwareDynamicLayoutComponentOptions()
    {
        Components = new Dictionary<Type, IDictionary<string, object>?>();
    }
}