using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.Blogging.Localization;

namespace SmartSoftware.Blogging
{
    [DependsOn(typeof(BloggingApplicationContractsSharedModule))]
    public class BloggingApplicationContractsModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
