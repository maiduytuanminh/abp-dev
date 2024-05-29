using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public abstract class SmartSoftwareBundleItemTagHelperService<TTagHelper, TService> : SmartSoftwareTagHelperService<TTagHelper>
    where TTagHelper : SmartSoftwareTagHelper<TTagHelper, TService>, IBundleItemTagHelper
    where TService : class, ISmartSoftwareTagHelperService<TTagHelper>
{
    protected SmartSoftwareTagHelperResourceService ResourceService { get; }

    protected SmartSoftwareBundleItemTagHelperService(SmartSoftwareTagHelperResourceService resourceService)
    {
        ResourceService = resourceService;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var tagHelperItems = context.Items.GetOrDefault(SmartSoftwareTagHelperConsts.ContextBundleItemListKey) as List<BundleTagHelperItem>;
        if (tagHelperItems != null)
        {
            output.SuppressOutput();
            tagHelperItems.Add(TagHelper.CreateBundleTagHelperItem());
        }
        else
        {
            await ResourceService.ProcessAsync(
                TagHelper.ViewContext,
                TagHelper,
                context,
                output,
                new List<BundleTagHelperItem>
                {
                        TagHelper.CreateBundleTagHelperItem()
                },
                TagHelper.GetNameOrNull()
            );
        }
    }
}
