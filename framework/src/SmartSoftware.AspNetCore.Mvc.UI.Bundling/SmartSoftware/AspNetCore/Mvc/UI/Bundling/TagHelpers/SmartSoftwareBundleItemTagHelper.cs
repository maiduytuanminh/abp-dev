using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public abstract class SmartSoftwareBundleItemTagHelper<TTagHelper, TTagHelperService> : SmartSoftwareTagHelper<TTagHelper, TTagHelperService>, IBundleItemTagHelper
    where TTagHelper : SmartSoftwareTagHelper<TTagHelper, TTagHelperService>, IBundleItemTagHelper
    where TTagHelperService : SmartSoftwareBundleItemTagHelperService<TTagHelper, TTagHelperService>
{
    /// <summary>
    /// A file path.
    /// </summary>
    public string? Src { get; set; }

    /// <summary>
    /// A bundle contributor type.
    /// </summary>
    public Type? Type { get; set; }

    protected SmartSoftwareBundleItemTagHelper(TTagHelperService service)
        : base(service)
    {
    }

    public string GetNameOrNull()
    {
        if (Type != null)
        {
            return Type.FullName!;
        }

        if (Src != null)
        {
            return Src
                .RemovePreFix("/")
                .RemovePostFix(StringComparison.OrdinalIgnoreCase, "." + GetFileExtension())
                .Replace("/", ".");
        }

        throw new SmartSoftwareException("ss-script tag helper requires to set either src or type!");
    }

    public BundleTagHelperItem CreateBundleTagHelperItem()
    {
        if (Type != null)
        {
            return new BundleTagHelperContributorTypeItem(Type);
        }

        if (Src != null)
        {
            return new BundleTagHelperFileItem(new BundleFile(Src));
        }

        throw new SmartSoftwareException("ss-script tag helper requires to set either src or type!");
    }

    protected abstract string GetFileExtension();
}
