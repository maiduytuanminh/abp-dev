using SmartSoftware.Testing;

namespace SmartSoftware.BlobStoring.Azure;

public class SmartSoftwareBlobStoringAzureTestCommonBase : SmartSoftwareIntegratedTest<SmartSoftwareBlobStoringAzureTestCommonModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}

public class SmartSoftwareBlobStoringAzureTestBase : SmartSoftwareIntegratedTest<SmartSoftwareBlobStoringAzureTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
