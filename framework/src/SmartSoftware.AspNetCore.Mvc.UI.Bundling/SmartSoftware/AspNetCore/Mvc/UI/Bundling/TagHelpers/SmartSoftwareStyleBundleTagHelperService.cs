namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public class SmartSoftwareStyleBundleTagHelperService : SmartSoftwareBundleTagHelperService<SmartSoftwareStyleBundleTagHelper, SmartSoftwareStyleBundleTagHelperService>
{
    public SmartSoftwareStyleBundleTagHelperService(SmartSoftwareTagHelperStyleService resourceHelper)
        : base(resourceHelper)
    {
    }
}
