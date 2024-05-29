using SmartSoftware.Modularity;

namespace SmartSoftware.Minify;

[DependsOn(
    typeof(SmartSoftwareMinifyModule),
    typeof(SmartSoftwareTestBaseModule))]
public class SmartSoftwareMinifyTestModule : SmartSoftwareModule
{
}
