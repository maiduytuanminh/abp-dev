using SmartSoftware.Testing;

namespace SmartSoftware.BlobStoring;

public abstract class SmartSoftwareBlobStoringTestBase : SmartSoftwareIntegratedTest<SmartSoftwareBlobStoringTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
