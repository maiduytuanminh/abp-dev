using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.Blogging.Admin;
using SmartSoftware.Blogging.EntityFrameworkCore;

namespace SmartSoftware.Blogging
{
    [DependsOn(
        typeof(BloggingApplicationModule),
        typeof(BloggingAdminApplicationModule),
        typeof(BloggingEntityFrameworkCoreTestModule),
        typeof(BloggingTestBaseModule))]
    public class BloggingApplicationTestModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAlwaysAllowAuthorization();
        }
    }
}
