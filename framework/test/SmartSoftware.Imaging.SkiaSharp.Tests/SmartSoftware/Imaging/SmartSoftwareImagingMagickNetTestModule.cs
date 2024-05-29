using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.Imaging;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareImagingSkiaSharpModule),
    typeof(SmartSoftwareTestBaseModule)
)]
public class SmartSoftwareImagingSkiaSharpTestModule : SmartSoftwareModule
{

}
