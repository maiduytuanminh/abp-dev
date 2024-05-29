using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.CmsKit.Contents;
using SmartSoftware.CmsKit.Public.Pages;
using SmartSoftware.CmsKit.Web.Contents;
using SmartSoftware.CmsKit.Web.Pages;

namespace SmartSoftware.CmsKit.Public.Web.Pages.Public.CmsKit.Pages;

public class IndexModel : CommonPageModel
{
    [BindProperty(SupportsGet = true)]
    public string Slug { get; set; }

    protected IPagePublicAppService PagePublicAppService { get; }

    protected ContentParser ContentParser { get; }

    public PageViewModel ViewModel { get; private set; }

    public IndexModel(IPagePublicAppService pagePublicAppService, ContentParser contentParser)
    {
        PagePublicAppService = pagePublicAppService;
        ContentParser = contentParser;
    }

    public virtual async Task<IActionResult> OnGetAsync()
    {
        var pageDto = await PagePublicAppService.FindBySlugAsync(Slug);
        ViewModel = ObjectMapper.Map<PageDto, PageViewModel>(pageDto);
        if (ViewModel == null)
        {
            return NotFound();
        }
        
        ViewModel.ContentFragments = await ContentParser.ParseAsync(pageDto.Content);

        return Page();
    }
}
