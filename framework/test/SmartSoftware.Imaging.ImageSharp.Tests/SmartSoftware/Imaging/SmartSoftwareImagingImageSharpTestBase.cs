using SmartSoftware.Testing;

namespace SmartSoftware.Imaging;

public abstract class SmartSoftwareImagingImageSharpTestBase : SmartSoftwareIntegratedTest<SmartSoftwareImagingImageSharpTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}