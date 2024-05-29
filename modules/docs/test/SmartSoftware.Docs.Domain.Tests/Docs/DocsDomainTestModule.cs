using SmartSoftware.Docs.EntityFrameworkCore;
using SmartSoftware.Modularity;

namespace SmartSoftware.Docs
{
    [DependsOn(
        typeof(DocsEntityFrameworkCoreTestModule),
        typeof(DocsTestBaseModule)
        )]
    public class DocsDomainTestModule : SmartSoftwareModule
    {
        
    }
}
