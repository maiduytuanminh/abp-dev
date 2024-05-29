namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public class SmartSoftwareScriptBundleTagHelperService : SmartSoftwareBundleTagHelperService<SmartSoftwareScriptBundleTagHelper, SmartSoftwareScriptBundleTagHelperService>
{
    public SmartSoftwareScriptBundleTagHelperService(SmartSoftwareTagHelperScriptService resourceHelper)
        : base(resourceHelper)
    {
    }
}
