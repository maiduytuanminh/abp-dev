﻿using SmartSoftware.AspNetCore.Mvc.UI.Bundling;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.ChartJs;

public class ChartjsScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add("/libs/chart.js/chart.umd.js");
    }
}
