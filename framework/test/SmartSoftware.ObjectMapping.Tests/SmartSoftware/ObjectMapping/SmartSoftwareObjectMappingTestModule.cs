using SmartSoftware.Modularity;

namespace SmartSoftware.ObjectMapping;

[DependsOn(
    typeof(SmartSoftwareObjectMappingModule),
    typeof(SmartSoftwareTestBaseModule)
    )]
public class SmartSoftwareObjectMappingTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTest1AutoObjectMappingProvider<MappingContext1>();
        context.Services.AddTest2AutoObjectMappingProvider<MappingContext2>();
    }
}
