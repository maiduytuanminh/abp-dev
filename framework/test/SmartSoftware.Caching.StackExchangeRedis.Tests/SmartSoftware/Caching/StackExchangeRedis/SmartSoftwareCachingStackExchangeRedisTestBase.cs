using SmartSoftware.Testing;

namespace SmartSoftware.Caching.StackExchangeRedis;

public abstract class SmartSoftwareCachingStackExchangeRedisTestBase : SmartSoftwareIntegratedTest<SmartSoftwareCachingStackExchangeRedisTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
