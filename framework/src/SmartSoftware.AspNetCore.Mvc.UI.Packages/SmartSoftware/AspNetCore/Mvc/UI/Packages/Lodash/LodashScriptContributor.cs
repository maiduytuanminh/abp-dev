using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Core;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.Lodash;

[DependsOn(typeof(CoreScriptContributor))]
public class LodashScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/lodash/lodash.min.js");
    }
}
