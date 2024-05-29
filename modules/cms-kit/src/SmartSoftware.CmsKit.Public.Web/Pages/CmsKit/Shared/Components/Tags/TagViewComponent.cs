﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.Widgets;
using SmartSoftware.CmsKit.Tags;

namespace SmartSoftware.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.Tags;

[Widget(
    StyleFiles = new[]
    {
            "/Pages/CmsKit/Shared/Components/Tags/default.css"
    })]
public class TagViewComponent : SmartSoftwareViewComponent
{
    protected readonly ITagAppService TagAppService;

    public TagViewComponent(ITagAppService tagAppService)
    {
        TagAppService = tagAppService;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync(
        string entityType,
        string entityId,
        string urlFormat)
    {
        var tagDtos = await TagAppService.GetAllRelatedTagsAsync(entityType, entityId);

        var viewModel = new TagViewModel
        {
            EntityId = entityId,
            EntityType = entityType,
            Tags = tagDtos,
            UrlFormat = urlFormat
        };

        return View("~/Pages/CmsKit/Shared/Components/Tags/Default.cshtml", viewModel);
    }

    public class TagViewModel
    {
        public List<TagDto> Tags { get; set; }
        public string EntityId { get; set; }
        public string EntityType { get; set; }
        public string UrlFormat { get; set; }
    }
}
