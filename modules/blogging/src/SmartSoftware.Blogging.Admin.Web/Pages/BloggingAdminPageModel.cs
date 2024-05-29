using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;

namespace SmartSoftware.Blogging.Admin.Pages
{
    public abstract class BloggingAdminPageModel : SmartSoftwarePageModel
    {
        public BloggingAdminPageModel()
        {
            ObjectMapperContext = typeof(BloggingAdminWebModule);
        }
    }
}
