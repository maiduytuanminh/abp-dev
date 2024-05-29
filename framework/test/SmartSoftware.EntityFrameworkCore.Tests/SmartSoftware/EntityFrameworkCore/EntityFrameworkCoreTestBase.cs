using SmartSoftware.TestApp.Testing;

namespace SmartSoftware.EntityFrameworkCore;

public abstract class EntityFrameworkCoreTestBase : TestAppTestBase<SmartSoftwareEntityFrameworkCoreTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
