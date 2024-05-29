using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.TagHelpers;

[HtmlTargetElement("ss-style", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SmartSoftwareStyleTagHelper : SmartSoftwareBundleItemTagHelper<SmartSoftwareStyleTagHelper, SmartSoftwareStyleTagHelperService>, IBundleItemTagHelper
{
    [HtmlAttributeName("preload")]
    public bool Preload { get; set; }

    public SmartSoftwareStyleTagHelper(SmartSoftwareStyleTagHelperService service)
        : base(service)
    {

    }

    protected override string GetFileExtension()
    {
        return "css";
    }
}
