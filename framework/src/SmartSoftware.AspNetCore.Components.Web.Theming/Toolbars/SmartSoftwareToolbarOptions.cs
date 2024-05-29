using System.Collections.Generic;
using JetBrains.Annotations;

namespace SmartSoftware.AspNetCore.Components.Web.Theming.Toolbars;

public class SmartSoftwareToolbarOptions
{
    [NotNull]
    public List<IToolbarContributor> Contributors { get; }

    public SmartSoftwareToolbarOptions()
    {
        Contributors = new List<IToolbarContributor>();
    }
}
