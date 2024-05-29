using SmartSoftware.Testing;

namespace SmartSoftware.BlobStoring.Aliyun;

public class SmartSoftwareBlobStoringAliyunTestCommonBase : SmartSoftwareIntegratedTest<SmartSoftwareBlobStoringAliyunTestCommonModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
public class SmartSoftwareBlobStoringAliyunTestBase : SmartSoftwareIntegratedTest<SmartSoftwareBlobStoringAliyunTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
