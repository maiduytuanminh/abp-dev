using SmartSoftware.Application;
using SmartSoftware.Autofac;
using SmartSoftware.ExceptionHandling;
using SmartSoftware.Modularity;

namespace SmartSoftware.GlobalFeatures;

[DependsOn(typeof(SmartSoftwareAutofacModule))]
[DependsOn(typeof(SmartSoftwareGlobalFeaturesModule))]
[DependsOn(typeof(SmartSoftwareDddApplicationModule))]
[DependsOn(typeof(SmartSoftwareExceptionHandlingModule))]
public class GlobalFeatureTestModule : SmartSoftwareModule
{

}
