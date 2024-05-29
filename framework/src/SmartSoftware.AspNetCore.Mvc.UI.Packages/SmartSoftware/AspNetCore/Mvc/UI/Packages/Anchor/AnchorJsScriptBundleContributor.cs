using System;
using System.Collections.Generic;
using System.Text;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.JQuery;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.Anchor;

[DependsOn(typeof(JQueryScriptContributor))]
public class AnchorJsScriptBundleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/anchor-js/anchor.js");
    }
}
