using SmartSoftware.Application;
using SmartSoftware.Modularity;

namespace SmartSoftware.Docs
{
    [DependsOn(
        typeof(DocsDomainSharedModule),
        typeof(SmartSoftwareDddApplicationContractsModule)
        )]
    public class DocsApplicationContractsModule : SmartSoftwareModule
    {
        
    }
}
