using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.Imaging;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareImagingImageSharpModule),
    typeof(SmartSoftwareTestBaseModule)
)]

public class SmartSoftwareImagingImageSharpTestModule : SmartSoftwareModule
{

}