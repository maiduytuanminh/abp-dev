using SmartSoftware.Testing;

namespace SmartSoftware.Imaging;

public abstract class SmartSoftwareImagingAbstractionsTestBase : SmartSoftwareIntegratedTest<SmartSoftwareImagingAbstractionsTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}