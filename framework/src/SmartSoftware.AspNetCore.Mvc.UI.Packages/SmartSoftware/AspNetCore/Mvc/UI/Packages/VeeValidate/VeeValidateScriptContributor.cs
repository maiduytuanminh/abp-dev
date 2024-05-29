using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Vue;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.VeeValidate;

[DependsOn(typeof(VueScriptContributor))]
public class VeeValidateScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add("/libs/vee-validate/vee-validate.full.min.js");
    }
}
