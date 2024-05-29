namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public class SmartSoftwareStyleTagHelperService : SmartSoftwareBundleItemTagHelperService<SmartSoftwareStyleTagHelper, SmartSoftwareStyleTagHelperService>
{
    public SmartSoftwareStyleTagHelperService(SmartSoftwareTagHelperStyleService resourceService)
        : base(resourceService)
    {
    }
}
