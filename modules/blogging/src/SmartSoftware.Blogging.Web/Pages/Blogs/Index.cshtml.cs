using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Blogs.Dtos;

namespace SmartSoftware.Blogging.Pages.Blogs
{
    public class IndexModel : SmartSoftwarePageModel
    {
        private readonly IBlogAppService _blogAppService;
        private readonly BloggingUrlOptions _blogOptions;

        public IReadOnlyList<BlogDto> Blogs { get; private set; }

        public IndexModel(IBlogAppService blogAppService, IOptions<BloggingUrlOptions> blogOptions)
        {
            _blogAppService = blogAppService;
            _blogOptions = blogOptions.Value;
        }

        public virtual async Task<IActionResult> OnGetAsync()
        {
            if (_blogOptions.SingleBlogMode.Enabled)
            {
                return RedirectToPage("./Posts/Index");
            }
            
            var result = await _blogAppService.GetListAsync();

            if (result.Items.Count == 1)
            {
                var blog = result.Items[0];
                return RedirectToPage("./Posts/Index", new { blogShortName = blog.ShortName });
            }

            Blogs = result.Items;

            return Page();
        }
    }
}
