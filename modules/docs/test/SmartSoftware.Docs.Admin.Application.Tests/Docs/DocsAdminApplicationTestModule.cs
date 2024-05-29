using SmartSoftware.Modularity;
using SmartSoftware.Docs.Admin;

namespace SmartSoftware.Docs
{
    [DependsOn(
        typeof(DocsAdminApplicationModule),
        typeof(DocsDomainTestModule)
    )]
    public class DocsAdminApplicationTestModule : SmartSoftwareModule
    {

    }
}
