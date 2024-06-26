﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.CmsKit.Public.Blogs;

namespace SmartSoftware.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.Blogs.BlogPostComment;

[ViewComponent(Name = "CmsDefaultBlogPostComment")]
public class DefaultBlogPostCommentViewComponent : SmartSoftwareViewComponent
{
    protected IBlogPostPublicAppService BlogPostPublicAppService;

    public DefaultBlogPostCommentViewComponent(IBlogPostPublicAppService blogPostPublicAppService)
    {
        BlogPostPublicAppService = blogPostPublicAppService;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync(string entityType, string entityId)
    {
        return View(
            "~/Pages/CmsKit/Shared/Components/Blogs/BlogPostComment/Default.cshtml",
            new DefaultBlogPostCommentViewModel
            {
                EntityType = entityType,
                EntityId = entityId
            });
    }

    public class DefaultBlogPostCommentViewModel
    {
        public string EntityType { get; set; }
        public string EntityId { get; set; }
    }
}
