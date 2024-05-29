using System;
using System.Collections.Generic;
using System.Text;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.MalihuCustomScrollbar;

public class MalihuCustomScrollbarPluginStyleBundleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css");
    }
}
