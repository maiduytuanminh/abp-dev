using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.Imaging;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareImagingAbstractionsModule),
    typeof(SmartSoftwareTestBaseModule)
)]
public class SmartSoftwareImagingAbstractionsTestModule : SmartSoftwareModule
{

}