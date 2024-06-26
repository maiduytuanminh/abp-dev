﻿using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.Select2;

public class Select2StyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/select2/css/select2.min.css");
    }
}
