using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.TagHelpers;

[HtmlTargetElement("ss-script", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SmartSoftwareScriptTagHelper : SmartSoftwareBundleItemTagHelper<SmartSoftwareScriptTagHelper, SmartSoftwareScriptTagHelperService>, IBundleItemTagHelper
{
    [HtmlAttributeName("defer")]
    public bool Defer { get; set; }

    public SmartSoftwareScriptTagHelper(SmartSoftwareScriptTagHelperService service)
        : base(service)
    {

    }

    protected override string GetFileExtension()
    {
        return "js";
    }
}
