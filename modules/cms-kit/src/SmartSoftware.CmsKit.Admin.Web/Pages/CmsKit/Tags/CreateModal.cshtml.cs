using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SmartSoftware.ObjectExtending;
using SmartSoftware.Validation;
using SmartSoftware.CmsKit.Admin.Tags;
using SmartSoftware.CmsKit.Tags;

namespace SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Tags;

public class CreateModalModel : CmsKitAdminPageModel
{
    protected ITagAdminAppService TagAdminAppService { get; }

    [BindProperty]
    public TagCreateViewModel ViewModel { get; set; }

    public List<SelectListItem> TagDefinitions { get; set; }

    public CreateModalModel(ITagAdminAppService tagAdminAppService)
    {
        TagAdminAppService = tagAdminAppService;
        ViewModel = new TagCreateViewModel();
    }

    public async Task OnGetAsync()
    {
        var definitions = await TagAdminAppService.GetTagDefinitionsAsync();

        TagDefinitions = definitions.Select(s => new SelectListItem(s.DisplayName, s.EntityType)).ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var tagCreateDto = ObjectMapper.Map<TagCreateViewModel, TagCreateDto>(ViewModel);

        await TagAdminAppService.CreateAsync(tagCreateDto);

        return NoContent();
    }

    [AutoMap(typeof(TagCreateDto), ReverseMap = true)]
    public class TagCreateViewModel : ExtensibleObject
    {
        [DynamicMaxLength(typeof(TagConsts), nameof(TagConsts.MaxEntityTypeLength))]
        [Required]
        [SelectItems(nameof(TagDefinitions))]
        public string EntityType { get; set; }

        [DynamicMaxLength(typeof(TagConsts), nameof(TagConsts.MaxNameLength))]
        [Required]
        public string Name { get; set; }
    }
}
