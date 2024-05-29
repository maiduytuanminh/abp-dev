using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.Localization;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.Bootstrap;

public class BootstrapStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        if (CultureHelper.IsRtl)
        {
            context.Files.AddIfNotContains("/libs/bootstrap/css/bootstrap.rtl.css");
        }
        else
        {
            context.Files.AddIfNotContains("/libs/bootstrap/css/bootstrap.css");
        }
    }
}
