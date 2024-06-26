﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SmartSoftware.ObjectExtending;
using SmartSoftware.Validation;
using SmartSoftware.CmsKit.Admin.Blogs;
using SmartSoftware.CmsKit.Blogs;

namespace SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Blogs;

public class CreateModalModel : CmsKitAdminPageModel
{
    protected IBlogAdminAppService BlogAdminAppService { get; }

    [BindProperty]
    public CreateBlogViewModel ViewModel { get; set; }

    public CreateModalModel(IBlogAdminAppService blogAdminAppService)
    {
        BlogAdminAppService = blogAdminAppService;
        ViewModel = new CreateBlogViewModel();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateBlogViewModel, CreateBlogDto>(ViewModel);

        await BlogAdminAppService.CreateAsync(dto);

        return NoContent();
    }
    
    public class CreateBlogViewModel : ExtensibleObject
    {
        [Required]
        [DynamicMaxLength(typeof(BlogConsts), nameof(BlogConsts.MaxNameLength))]
        public string Name { get; set; }

        [DynamicMaxLength(typeof(BlogConsts), nameof(BlogConsts.MaxSlugLength))]
        [Required]
        public string Slug { get; set; }
    }
}
