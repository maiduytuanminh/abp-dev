using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.Blogging.Localization;

namespace SmartSoftware.Blogging.Admin.Pages.Blogging
{
        public abstract class BloggingAdminPage : SmartSoftwarePage
        {
            [RazorInject]
            public IHtmlLocalizer<BloggingResource> L { get; set; }

            public const string DefaultTitle = "Blogging";

            public const int MaxShortContentLength = 200;
    }
}
