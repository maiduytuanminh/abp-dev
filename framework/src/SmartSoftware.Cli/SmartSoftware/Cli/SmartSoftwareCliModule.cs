using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.Cli;

[DependsOn(
    typeof(SmartSoftwareCliCoreModule),
    typeof(SmartSoftwareAutofacModule)
)]
public class SmartSoftwareCliModule : SmartSoftwareModule
{

}
