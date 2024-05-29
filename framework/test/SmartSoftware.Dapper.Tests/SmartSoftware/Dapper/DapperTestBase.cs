using SmartSoftware.Testing;

namespace SmartSoftware.Dapper;

public abstract class DapperTestBase : SmartSoftwareIntegratedTest<SmartSoftwareDapperTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
