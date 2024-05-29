namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Carousel;

public class SmartSoftwareCarouselTagHelper : SmartSoftwareTagHelper<SmartSoftwareCarouselTagHelper, SmartSoftwareCarouselTagHelperService>
{
    public string? Id { get; set; }

    public bool? Crossfade { get; set; }

    public bool? Controls { get; set; }

    public bool? Indicators { get; set; }

    public SmartSoftwareCarouselTagHelper(SmartSoftwareCarouselTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
