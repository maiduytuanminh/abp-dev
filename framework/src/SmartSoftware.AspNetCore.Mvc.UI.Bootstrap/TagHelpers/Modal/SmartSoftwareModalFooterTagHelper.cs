using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal;

[HtmlTargetElement("ss-modal-footer")]
public class SmartSoftwareModalFooterTagHelper : SmartSoftwareTagHelper<SmartSoftwareModalFooterTagHelper, SmartSoftwareModalFooterTagHelperService>
{
    public SmartSoftwareModalButtons Buttons { get; set; }
    public ButtonsAlign ButtonAlignment { get; set; } = ButtonsAlign.Default;

    public SmartSoftwareModalFooterTagHelper(SmartSoftwareModalFooterTagHelperService tagHelperService)
        : base(tagHelperService)
    {
    }
}
