using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.CmsKit.Admin.Contents;
using SmartSoftware.CmsKit.Contents;
using SmartSoftware.CmsKit.Web.Contents;

namespace SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Contents;

public class AddWidgetModal : SmartSoftwarePageModel
{

    [BindProperty]
    public ContentViewModel ViewModel { get; set; }

    public List<SelectListItem> Widgets { get; set; } = new();

    private readonly CmsKitContentWidgetOptions _options;

    public AddWidgetModal(IOptions<CmsKitContentWidgetOptions> options)
    {
        _options = options.Value;
    }

    public async Task OnGetAsync()
    {
        var widgets = _options.WidgetConfigs
                .Select(n =>
                    new ContentWidgetDto
                    {
                        Key = n.Key,
                        Details = new WidgetDetailDto() { EditorComponentName = n.Value.EditorComponentName, Name = n.Value.Name },

                    }).ToList();
        
        ViewModel = new ContentViewModel()
        {
            Details = widgets.Select(p => p.Details).ToList()
        };

        Widgets = new List<SelectListItem>() { new(string.Empty, string.Empty) };
        Widgets.AddRange(widgets
            .Select(w => new SelectListItem(w.Key, w.Details.Name))
            .ToList());
    }

    public class ContentViewModel
    {
        [SelectItems(nameof(Widgets))]
        public string Widget { get; set; }

        public List<WidgetDetailDto> Details { get; set; }
    }
}
