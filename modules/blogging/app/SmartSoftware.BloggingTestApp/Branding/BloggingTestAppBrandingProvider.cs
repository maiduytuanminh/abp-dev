using Microsoft.AspNetCore.Mvc.Localization;
using SmartSoftware.Ui.Branding;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Blogging.Localization;

namespace SmartSoftware.BloggingTestApp.Branding
{
    [Dependency(ReplaceServices = true)]
    public class BloggingTestAppBrandingProvider : DefaultBrandingProvider
    {
        public IHtmlLocalizer<BloggingResource> L { get; set; }
        public BloggingTestAppBrandingProvider(IHtmlLocalizer<BloggingResource> localizer)
        {
            L = localizer;
        }
        public override string AppName => L["Blogs"].Value;
    }
}
