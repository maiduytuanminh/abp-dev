using System.Collections.Generic;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;

public class SmartSoftwareErrorPageOptions
{
    public readonly IDictionary<string, string> ErrorViewUrls;

    public SmartSoftwareErrorPageOptions()
    {
        ErrorViewUrls = new Dictionary<string, string>();
    }
}
