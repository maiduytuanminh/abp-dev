using SmartSoftware.Testing;

namespace SmartSoftware.BlobStoring.Aws;

public class SmartSoftwareBlobStoringAwsTestCommonBase : SmartSoftwareIntegratedTest<SmartSoftwareBlobStoringAwsTestCommonModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}

public class SmartSoftwareBlobStoringAwsTestBase : SmartSoftwareIntegratedTest<SmartSoftwareBlobStoringAwsTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
