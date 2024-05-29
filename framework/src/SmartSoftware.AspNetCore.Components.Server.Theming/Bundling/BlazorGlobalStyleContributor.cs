using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Bootstrap;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.FontAwesome;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Components.Server.Theming.Bundling;

[DependsOn(
    typeof(BootstrapStyleContributor),
    typeof(FontAwesomeStyleContributor)
)]
public class BlazorGlobalStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/_content/SmartSoftware.AspNetCore.Components.Web/libs/smartsoftware/css/ss.css");
        context.Files.AddIfNotContains("/_content/Blazorise/blazorise.css");
        context.Files.AddIfNotContains("/_content/Blazorise.Bootstrap5/blazorise.bootstrap5.css");
        context.Files.AddIfNotContains("/_content/Blazorise.Snackbar/blazorise.snackbar.css");
        context.Files.AddIfNotContains("/_content/SmartSoftware.BlazoriseUI/smartsoftware.blazoriseui.css");
    }
}
