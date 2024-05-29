﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSoftware.Domain.Entities;
using SmartSoftware.ObjectExtending;
using SmartSoftware.Validation;
using SmartSoftware.CmsKit.Admin.Tags;
using SmartSoftware.CmsKit.Tags;

namespace SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Tags;

public class EditModalModel : CmsKitAdminPageModel
{
    protected ITagAdminAppService TagAdminAppService { get; }

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public TagEditViewModel ViewModel { get; set; }

    public EditModalModel(ITagAdminAppService tagAdminAppService)
    {
        this.TagAdminAppService = tagAdminAppService;
    }

    public async Task OnGetAsync()
    {
        var dto = await TagAdminAppService.GetAsync(Id);

        ViewModel = ObjectMapper.Map<TagDto, TagEditViewModel>(dto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var tagDto = ObjectMapper.Map<TagEditViewModel, TagUpdateDto>(ViewModel);
        await TagAdminAppService.UpdateAsync(Id, tagDto);
        return NoContent();
    }

    [AutoMap(typeof(TagDto))]
    [AutoMap(typeof(TagUpdateDto), ReverseMap = true)]
    public class TagEditViewModel : ExtensibleObject, IHasConcurrencyStamp
    {
        [Required]
        [DynamicMaxLength(typeof(TagConsts), nameof(TagConsts.MaxNameLength))]
        public string Name { get; set; }

        [HiddenInput]
        public string ConcurrencyStamp { get; set; }
    }
}
