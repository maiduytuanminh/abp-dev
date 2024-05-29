using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;

namespace SmartSoftware.Docs.Admin.Pages.Docs.Admin
{
    public abstract class DocsAdminPageModel : SmartSoftwarePageModel
    {
        public DocsAdminPageModel()
        {
            ObjectMapperContext = typeof(DocsAdminWebModule);
        }
    }
}