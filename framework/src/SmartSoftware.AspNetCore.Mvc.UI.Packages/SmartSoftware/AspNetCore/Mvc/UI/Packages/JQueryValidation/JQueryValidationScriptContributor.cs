using System.Collections.Generic;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.JQuery;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.JQueryValidation;

[DependsOn(typeof(JQueryScriptContributor))]
public class JQueryValidationScriptContributor : BundleContributor
{
    public const string PackageName = "jquery-validation";

    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/jquery-validation/jquery.validate.js");
    }

    public override void ConfigureDynamicResources(BundleConfigurationContext context)
    {
        var fileName = context.LazyServiceProvider.LazyGetRequiredService<IOptions<SmartSoftwareLocalizationOptions>>().Value.GetCurrentUICultureLanguageFilesMap(PackageName);
        var filePath = $"/libs/jquery-validation/localization/messages_{fileName}.js";
        if (context.FileProvider.GetFileInfo(filePath).Exists)
        {
            context.Files.AddIfNotContains(filePath);
        }
    }
}
