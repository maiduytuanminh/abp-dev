using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.JQueryValidation;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.JQueryValidationUnobtrusive;

[DependsOn(typeof(JQueryValidationScriptContributor))]
public class JQueryValidationUnobtrusiveScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js");
    }
}
