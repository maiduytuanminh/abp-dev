using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using SmartSoftware.Ui.Branding;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Docs.Localization;

namespace SmartSoftwareDocs.Web.Branding
{
    [Dependency(ReplaceServices = true)]
    public class SmartSoftwareDocsBrandingProvider : DefaultBrandingProvider
    {
        public SmartSoftwareDocsBrandingProvider(IConfiguration configuration, IStringLocalizer<DocsResource> localizer)
        {
            AppName = localizer["DocsTitle"];

            if (configuration["LogoUrl"] != null)
            {
                LogoUrl = configuration["LogoUrl"];
            }
        }

        public override string AppName { get; }

        public override string LogoUrl { get; }
    }
}
