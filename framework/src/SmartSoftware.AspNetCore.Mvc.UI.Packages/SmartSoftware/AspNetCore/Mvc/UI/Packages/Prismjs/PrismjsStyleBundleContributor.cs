using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.Prismjs;

public class PrismjsStyleBundleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/prismjs/themes/prism.css");
    }
}
