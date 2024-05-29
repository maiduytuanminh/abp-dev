using SmartSoftware.Modularity;
using SmartSoftware.Blogging.EntityFrameworkCore;

namespace SmartSoftware.Blogging
{
    [DependsOn(
        typeof(BloggingEntityFrameworkCoreTestModule),
        typeof(BloggingTestBaseModule)
    )]
    public class BloggingDomainTestModule : SmartSoftwareModule
    {
    }
}