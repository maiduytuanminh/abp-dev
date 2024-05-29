using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public abstract class SmartSoftwareBundleTagHelperService<TTagHelper, TService> : SmartSoftwareTagHelperService<TTagHelper>
    where TTagHelper : SmartSoftwareTagHelper<TTagHelper, TService>, IBundleTagHelper
    where TService : class, ISmartSoftwareTagHelperService<TTagHelper>
{
    protected SmartSoftwareTagHelperResourceService ResourceService { get; }

    protected SmartSoftwareBundleTagHelperService(SmartSoftwareTagHelperResourceService resourceService)
    {
        ResourceService = resourceService;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        await ResourceService.ProcessAsync(
            TagHelper.ViewContext,
            TagHelper,
            context,
            output,
            await GetBundleItems(context, output),
            TagHelper.GetNameOrNull()
        );
    }

    protected virtual async Task<List<BundleTagHelperItem>> GetBundleItems(TagHelperContext context, TagHelperOutput output)
    {
        var bundleItems = new List<BundleTagHelperItem>();
        context.Items[SmartSoftwareTagHelperConsts.ContextBundleItemListKey] = bundleItems;
        await output.GetChildContentAsync(); //TODO: Is there a way of executing children without getting content?
        return bundleItems;
    }
}
