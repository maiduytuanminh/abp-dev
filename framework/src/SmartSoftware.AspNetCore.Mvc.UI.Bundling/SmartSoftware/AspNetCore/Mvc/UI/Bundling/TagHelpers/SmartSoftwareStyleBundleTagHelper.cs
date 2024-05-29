using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.TagHelpers;

[HtmlTargetElement("ss-style-bundle", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SmartSoftwareStyleBundleTagHelper : SmartSoftwareBundleTagHelper<SmartSoftwareStyleBundleTagHelper, SmartSoftwareStyleBundleTagHelperService>, IBundleTagHelper
{
    [HtmlAttributeName("preload")]
    public bool Preload { get; set; }

    public SmartSoftwareStyleBundleTagHelper(SmartSoftwareStyleBundleTagHelperService service)
        : base(service)
    {
    }
}
