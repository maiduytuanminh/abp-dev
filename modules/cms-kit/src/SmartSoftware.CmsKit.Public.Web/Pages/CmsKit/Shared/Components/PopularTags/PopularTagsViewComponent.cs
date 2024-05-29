using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.Widgets;
using SmartSoftware.CmsKit.Tags;

namespace SmartSoftware.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.PopularTags;

[Widget(
    StyleFiles = new[]
    {
        "/Pages/CmsKit/Shared/Components/PopularTags/default.css"
    })]
public class PopularTagsViewComponent : SmartSoftwareViewComponent
{
    private readonly ITagAppService _tagAppService;
    
    public PopularTagsViewComponent(ITagAppService tagAppService)
    {
        _tagAppService = tagAppService;
    }
    
    public virtual async Task<IViewComponentResult> InvokeAsync(string entityType, int maxCount, string urlFormat)
    {
        var model = new PopularTagsViewModel
        {
            Tags = await _tagAppService.GetPopularTagsAsync(entityType, maxCount),
            UrlFormat = urlFormat
        };
        return View("~/Pages/CmsKit/Shared/Components/PopularTags/Default.cshtml", model);
    }

    public class PopularTagsViewModel
    {
        public List<PopularTagDto> Tags { get; set; }
        public string UrlFormat { get; set; }
    }
}