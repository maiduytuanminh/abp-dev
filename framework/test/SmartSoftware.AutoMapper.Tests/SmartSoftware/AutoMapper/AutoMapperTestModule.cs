using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;

namespace SmartSoftware.AutoMapper;

[DependsOn(
    typeof(SmartSoftwareAutoMapperModule),
    typeof(SmartSoftwareObjectExtendingTestModule)
)]
public class AutoMapperTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddMaps<AutoMapperTestModule>();
        });
    }
}
