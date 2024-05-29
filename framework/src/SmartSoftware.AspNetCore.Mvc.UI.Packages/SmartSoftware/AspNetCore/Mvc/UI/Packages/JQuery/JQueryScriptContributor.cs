using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Core;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.JQuery;

[DependsOn(typeof(CoreScriptContributor))]
public class JQueryScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/jquery/jquery.js");
        context.Files.AddIfNotContains("/libs/smartsoftware/jquery/ss.jquery.js");
    }
}
