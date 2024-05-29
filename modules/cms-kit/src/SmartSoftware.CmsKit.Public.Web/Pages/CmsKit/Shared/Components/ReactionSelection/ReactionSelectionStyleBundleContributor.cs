using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;

namespace SmartSoftware.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.ReactionSelection;

public class ReactionSelectionStyleBundleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/Pages/CmsKit/Shared/Components/ReactionSelection/default.css");
    }
}
