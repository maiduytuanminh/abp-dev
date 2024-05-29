using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.Blogging.Localization;

namespace SmartSoftware.Blogging.Admin
{
    [DependsOn(typeof(BloggingApplicationContractsSharedModule))]
    public class BloggingAdminApplicationContractsModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
