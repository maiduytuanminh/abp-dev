using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.JQuery;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.StarRatingSvg;

[DependsOn(typeof(JQueryScriptContributor))]
public class StarRatingSvgScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/star-rating-svg/js/jquery.star-rating-svg.min.js");
    }
}
