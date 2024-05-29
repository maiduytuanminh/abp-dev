using System.Collections.Generic;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.JQuery;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.Timeago;

[DependsOn(typeof(JQueryScriptContributor))]
public class TimeagoScriptContributor : BundleContributor
{
    public const string PackageName = "jquery.timeago";

    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/timeago/jquery.timeago.js");
    }

    public override void ConfigureDynamicResources(BundleConfigurationContext context)
    {
        var fileName = context.LazyServiceProvider.LazyGetRequiredService<IOptions<SmartSoftwareLocalizationOptions>>().Value.GetCurrentUICultureLanguageFilesMap(PackageName);
        var filePath = $"/libs/timeago/locales/jquery.timeago.{fileName}.js";
        if (context.FileProvider.GetFileInfo(filePath).Exists)
        {
            context.Files.Add(filePath);
        }
    }
}
