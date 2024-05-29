using System.Collections.Generic;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.JQuery;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Moment;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.BootstrapDaterangepicker;

[DependsOn(typeof(JQueryScriptContributor))]
[DependsOn(typeof(MomentScriptContributor))]
public class BootstrapDaterangepickerScriptContributor : BundleContributor
{
    public const string PackageName = "bootstrap-daterangepicker";

    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/bootstrap-daterangepicker/daterangepicker.js");
    }
}
