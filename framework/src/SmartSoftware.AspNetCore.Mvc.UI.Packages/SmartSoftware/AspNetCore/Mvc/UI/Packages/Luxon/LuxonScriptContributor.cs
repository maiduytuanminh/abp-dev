using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.Luxon;

public class LuxonScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/luxon/luxon.min.js");
        context.Files.AddIfNotContains("/libs/smartsoftware/luxon/ss.luxon.js");
    }
}
