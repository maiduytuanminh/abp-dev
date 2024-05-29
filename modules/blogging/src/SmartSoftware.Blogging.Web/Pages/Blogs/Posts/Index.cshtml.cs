using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSoftware.Domain.Entities;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Blogs.Dtos;
using SmartSoftware.Blogging.Pages.Blogs.Shared.Helpers;
using SmartSoftware.Blogging.Posts;
using SmartSoftware.Blogging.Tagging;
using SmartSoftware.Blogging.Tagging.Dtos;

namespace SmartSoftware.Blogging.Pages.Blogs.Posts
{
    public class IndexModel : BloggingPageModel
    {
        private readonly IPostAppService _postAppService;
        private readonly IBlogAppService _blogAppService;
        private readonly ITagAppService _tagAppService;
        public BloggingUrlOptions BlogOptions { get; }

        [BindProperty(SupportsGet = true)]
        public string BlogShortName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TagName { get; set; }

        public BlogDto Blog { get; set; }

        public IReadOnlyList<PostWithDetailsDto> Posts { get; set; }

        public IReadOnlyList<TagDto> PopularTags { get; set; }

        public IndexModel(IPostAppService postAppService, IBlogAppService blogAppService, ITagAppService tagAppService, IOptions<BloggingUrlOptions> blogOptions)
        {
            _postAppService = postAppService;
            _blogAppService = blogAppService;
            _tagAppService = tagAppService;
            BlogOptions = blogOptions.Value;
        }

        public virtual async Task<ActionResult> OnGetAsync()
        {
            if (BlogNameControlHelper.IsProhibitedFileFormatName(BlogShortName))
            {
                return NotFound();
            }

            try
            {
                Blog = await GetBlogAsync(_blogAppService, BlogOptions, BlogShortName);
                
                if(Blog == null)
                {
                    return BlogNotFoundResult();
                }
                
                BlogShortName = Blog.ShortName;
            }
            catch (EntityNotFoundException)
            {
                return BlogNotFoundResult();
            }
            
            Posts = (await _postAppService.GetListByBlogIdAndTagNameAsync(Blog.Id, TagName)).Items;
            PopularTags = (await _tagAppService.GetPopularTagsAsync(Blog.Id, new GetPopularTagsInput {ResultCount = 10, MinimumPostCount = 2}));

            return Page();
        }
        
        protected virtual ActionResult BlogNotFoundResult()
        {
            if (BlogOptions.SingleBlogMode.Enabled)
            {
                return NotFound();
            }
                
            return RedirectToPage("/Blogs/Index");
        }
    }
}