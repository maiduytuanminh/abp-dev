using SmartSoftware.Testing;

namespace SmartSoftware.Json;

public abstract class SmartSoftwareJsonSystemTextJsonTestBase : SmartSoftwareIntegratedTest<SmartSoftwareJsonSystemTextJsonTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}

public abstract class SmartSoftwareJsonNewtonsoftJsonTestBase : SmartSoftwareIntegratedTest<SmartSoftwareJsonNewtonsoftTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
