using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.StarRatingSvg;
using SmartSoftware.Modularity;

namespace SmartSoftware.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.Rating;

[DependsOn(typeof(StarRatingSvgScriptContributor))]
public class RatingScriptBundleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/client-proxies/cms-kit-common-proxy.js");
        context.Files.AddIfNotContains("/client-proxies/cms-kit-proxy.js");
        context.Files.AddIfNotContains("/Pages/CmsKit/Shared/Components/Rating/default.js");
    }
}
