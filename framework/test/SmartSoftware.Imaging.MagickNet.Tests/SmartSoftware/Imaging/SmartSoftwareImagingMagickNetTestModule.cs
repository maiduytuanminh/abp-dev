using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.Imaging;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareImagingMagickNetModule),
    typeof(SmartSoftwareTestBaseModule)
)]
public class SmartSoftwareImagingMagickNetTestModule : SmartSoftwareModule
{

}