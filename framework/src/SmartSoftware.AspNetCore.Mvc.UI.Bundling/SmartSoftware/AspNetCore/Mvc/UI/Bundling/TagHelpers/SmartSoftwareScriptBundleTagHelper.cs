using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.TagHelpers;

[HtmlTargetElement("ss-script-bundle", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SmartSoftwareScriptBundleTagHelper : SmartSoftwareBundleTagHelper<SmartSoftwareScriptBundleTagHelper, SmartSoftwareScriptBundleTagHelperService>, IBundleTagHelper
{
    [HtmlAttributeName("defer")]
    public bool Defer { get; set; }

    public SmartSoftwareScriptBundleTagHelper(SmartSoftwareScriptBundleTagHelperService service)
        : base(service)
    {

    }
}
