using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

[HtmlTargetElement("a", Attributes = "ss-button", TagStructure = TagStructure.NormalOrSelfClosing)]
[HtmlTargetElement("input", Attributes = "ss-button", TagStructure = TagStructure.WithoutEndTag)]
public class SmartSoftwareLinkButtonTagHelper : SmartSoftwareTagHelper<SmartSoftwareLinkButtonTagHelper, SmartSoftwareLinkButtonTagHelperService>, IButtonTagHelperBase
{
    [HtmlAttributeName("ss-button")]
    public SmartSoftwareButtonType ButtonType { get; set; }

    public SmartSoftwareButtonSize Size { get; set; } = SmartSoftwareButtonSize.Default;

    public string? Text { get; set; }

    public string? Icon { get; set; }

    public bool? Disabled { get; set; }

    public FontIconType IconType { get; } = FontIconType.FontAwesome;

    public SmartSoftwareLinkButtonTagHelper(SmartSoftwareLinkButtonTagHelperService service)
        : base(service)
    {

    }
}
