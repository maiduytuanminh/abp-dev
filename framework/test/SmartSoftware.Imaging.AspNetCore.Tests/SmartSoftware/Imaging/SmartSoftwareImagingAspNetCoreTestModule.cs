using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.Imaging;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareImagingAspNetCoreModule),
    typeof(SmartSoftwareTestBaseModule)
)]
public class SmartSoftwareImagingAspNetCoreTestModule : SmartSoftwareModule
{

}