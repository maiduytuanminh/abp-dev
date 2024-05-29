using Microsoft.Extensions.DependencyInjection;

namespace SmartSoftware.Autofac;

public class Autofac_DependencyInjection_Standard_Tests : DependencyInjection_Standard_Tests
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
