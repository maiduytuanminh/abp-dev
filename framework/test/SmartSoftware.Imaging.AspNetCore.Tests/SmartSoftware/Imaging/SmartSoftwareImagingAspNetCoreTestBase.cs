using SmartSoftware.Testing;

namespace SmartSoftware.Imaging;

public abstract class SmartSoftwareImagingAspNetCoreTestBase : SmartSoftwareIntegratedTest<SmartSoftwareImagingAspNetCoreTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}