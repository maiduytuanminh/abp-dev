using SmartSoftware.Testing;

namespace SmartSoftware.Imaging;

public abstract class SmartSoftwareImagingMagickNetTestBase : SmartSoftwareIntegratedTest<SmartSoftwareImagingMagickNetTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}