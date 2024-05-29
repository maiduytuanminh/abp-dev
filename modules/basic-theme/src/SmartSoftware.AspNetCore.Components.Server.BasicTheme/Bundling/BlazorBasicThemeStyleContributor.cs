using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;

namespace SmartSoftware.AspNetCore.Components.Server.BasicTheme.Bundling;

public class BlazorBasicThemeStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/_content/SmartSoftware.AspNetCore.Components.Web.BasicTheme/libs/smartsoftware/css/theme.css");
    }
}
