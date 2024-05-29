namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public class SmartSoftwareScriptTagHelperService : SmartSoftwareBundleItemTagHelperService<SmartSoftwareScriptTagHelper, SmartSoftwareScriptTagHelperService>
{
    public SmartSoftwareScriptTagHelperService(SmartSoftwareTagHelperScriptService resourceService)
        : base(resourceService)
    {
    }
}
