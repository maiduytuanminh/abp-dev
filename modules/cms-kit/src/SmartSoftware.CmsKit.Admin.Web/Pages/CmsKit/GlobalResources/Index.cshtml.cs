using System;
using System.Threading.Tasks;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SmartSoftware.CmsKit.Admin.GlobalResources;

namespace SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.GlobalResources;

public class IndexModel : CmsKitAdminPageModel
{
    private readonly IGlobalResourceAdminAppService _globalResourceAdminAppService;

    [TextArea(Rows = 30)]
    public string ScriptContent { get; set; }

    [TextArea(Rows = 30)] 
    public string StyleContent { get; set; }

    public IndexModel(IGlobalResourceAdminAppService globalResourceAdminAppService)
    {
        _globalResourceAdminAppService = globalResourceAdminAppService;
    }
    
    public async Task OnGetAsync()
    {
        var resources = await _globalResourceAdminAppService.GetAsync();

        ScriptContent = resources.ScriptContent;
        StyleContent = resources.StyleContent;
    }
}
