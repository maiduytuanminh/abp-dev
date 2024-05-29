using SmartSoftware.Testing;

namespace SmartSoftware.MongoDB;

public abstract class MongoDbTestBase : SmartSoftwareIntegratedTest<SmartSoftwareMongoDbTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
