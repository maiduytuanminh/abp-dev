using SmartSoftware.Testing;

namespace SmartSoftware.Imaging;

public abstract class SmartSoftwareImagingSkiaSharpTestBase : SmartSoftwareIntegratedTest<SmartSoftwareImagingSkiaSharpTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
