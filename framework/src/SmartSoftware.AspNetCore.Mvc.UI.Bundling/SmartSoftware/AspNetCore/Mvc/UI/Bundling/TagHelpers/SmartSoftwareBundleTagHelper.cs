using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public abstract class SmartSoftwareBundleTagHelper<TTagHelper, TService> : SmartSoftwareTagHelper<TTagHelper, TService>, IBundleTagHelper
    where TTagHelper : SmartSoftwareTagHelper<TTagHelper, TService>
    where TService : class, ISmartSoftwareTagHelperService<TTagHelper>
{
    public string? Name { get; set; }

    protected SmartSoftwareBundleTagHelper(TService service)
        : base(service)
    {

    }

    public virtual string? GetNameOrNull()
    {
        return Name;
    }
}
