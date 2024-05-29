using SmartSoftware.Modularity;

namespace SmartSoftware.Cli;

[DependsOn(
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareCliCoreModule)
    )]
public class SmartSoftwareCliTestModule : SmartSoftwareModule
{

}
