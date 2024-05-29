using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.HighlightJs;

public class HighlightJsStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        //TODO: Make this configurable
        context.Files.AddIfNotContains("/libs/highlight.js/styles/github.min.css");
    }
}
