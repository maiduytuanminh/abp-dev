using SmartSoftware.Modularity;

namespace SmartSoftware.Threading;

[DependsOn(
    typeof(SmartSoftwareThreadingModule),
    typeof(SmartSoftwareTestBaseModule)
)]
public class SmartSoftwareThreadingTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}
