using SmartSoftware.Testing;

namespace SmartSoftware.Sms.Aliyun;

public class SmartSoftwareSmsAliyunTestBase : SmartSoftwareIntegratedTest<SmartSoftwareSmsAliyunTestsModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
