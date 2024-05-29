using SmartSoftware.Autofac;
using SmartSoftware.ExceptionHandling;
using SmartSoftware.Modularity;

namespace SmartSoftware.Features;

[DependsOn(
    typeof(SmartSoftwareFeaturesModule),
    typeof(SmartSoftwareExceptionHandlingModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareAutofacModule)
    )]
public class SmartSoftwareFeaturesTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

    }
}
