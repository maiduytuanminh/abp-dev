﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.Application.Dtos;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination;
using SmartSoftware.CmsKit.Contents;
using SmartSoftware.CmsKit.Public.Blogs;
using SmartSoftware.CmsKit.Users;

namespace SmartSoftware.CmsKit.Public.Web.Pages.Public.CmsKit.Blogs;

public class IndexModel : CmsKitPublicPageModelBase
{
    public const int PageSize = 12;

    [BindProperty(SupportsGet = true)]
    public string BlogSlug { get; set; }

    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    public Guid? AuthorId { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid? TagId { get; set; }

    public PagedResultDto<BlogPostCommonDto> Blogs { get; protected set; }

    public PagerModel PagerModel => new PagerModel(Blogs.TotalCount, Blogs.Items.Count, CurrentPage, PageSize, Request.Path.ToString());

    public CmsUserDto SelectedAuthor { get; protected set; }

    public string FilteredTagName { get; protected set; }

    protected IBlogPostPublicAppService BlogPostPublicAppService { get; }

    public IndexModel(IBlogPostPublicAppService blogPostPublicAppService)
    {
        BlogPostPublicAppService = blogPostPublicAppService;
    }

    public virtual async Task<IActionResult> OnGetAsync()
    {
        Blogs = await BlogPostPublicAppService.GetListAsync(
            BlogSlug,
            new BlogPostGetListInput
            {
                SkipCount = PageSize * (CurrentPage - 1),
                MaxResultCount = PageSize,
                AuthorId = AuthorId,
                TagId = TagId
            });

        if (AuthorId != null)
        {
            SelectedAuthor = await BlogPostPublicAppService.GetAuthorHasBlogPostAsync(AuthorId.Value);
        }

        if (TagId is not null)
        {
            FilteredTagName = await BlogPostPublicAppService.GetTagNameAsync(TagId.Value);
        }

        return Page();
    }
}