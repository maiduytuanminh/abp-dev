using SmartSoftware.Testing;

namespace SmartSoftware.BlobStoring.Minio;

public class SmartSoftwareBlobStoringMinioTestCommonBase : SmartSoftwareIntegratedTest<SmartSoftwareBlobStoringMinioTestCommonModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}

public class SmartSoftwareBlobStoringMinioTestBase : SmartSoftwareIntegratedTest<SmartSoftwareBlobStoringMinioTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
