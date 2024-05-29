namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Carousel;

public class SmartSoftwareCarouselItemTagHelper : SmartSoftwareTagHelper<SmartSoftwareCarouselItemTagHelper, SmartSoftwareCarouselItemTagHelperService>
{
    public bool? Active { get; set; }

    public string Src { get; set; } = default!;

    public string Alt { get; set; } = default!;

    public string? CaptionTitle { get; set; }

    public string? Caption { get; set; }

    public SmartSoftwareCarouselItemTagHelper(SmartSoftwareCarouselItemTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
