using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using System.Collections.Generic;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.CropperJs;

public class CropperJsScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/cropperjs/js/cropper.min.js");
    }
}
