using SmartSoftware.Modularity;

namespace SmartSoftware.Docs
{
    [DependsOn(
        typeof(DocsApplicationModule),
        typeof(DocsDomainTestModule)
        )]
    public class DocsApplicationTestModule : SmartSoftwareModule
    {

    }
}
