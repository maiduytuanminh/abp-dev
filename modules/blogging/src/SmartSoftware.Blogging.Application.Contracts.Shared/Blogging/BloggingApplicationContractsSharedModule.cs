using SmartSoftware.Application;
using SmartSoftware.Authorization;
using SmartSoftware.Modularity;

namespace SmartSoftware.Blogging
{
    [DependsOn(
        typeof(BloggingDomainSharedModule),
        typeof(SmartSoftwareDddApplicationContractsModule),
        typeof(SmartSoftwareAuthorizationAbstractionsModule)
        )]
    public class BloggingApplicationContractsSharedModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
