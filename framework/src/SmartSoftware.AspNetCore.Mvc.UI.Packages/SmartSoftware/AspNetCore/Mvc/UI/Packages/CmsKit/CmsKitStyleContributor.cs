using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.StarRatingSvg;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.CmsKit;

[DependsOn(typeof(StarRatingSvgStyleContributor))]
public class CmsKitStyleContributor : BundleContributor
{
}
