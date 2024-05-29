using System;
using System.Threading.Tasks;
using SmartSoftware.DynamicProxy;

namespace SmartSoftware.Autofac.Interception;

public class Autofac_Interception_Test : SmartSoftwareInterceptionTestBase<AutofacTestModule>
{
    protected override Task<Action<SmartSoftwareApplicationCreationOptions>> SetSmartSoftwareApplicationCreationOptionsAsync()
    {
        return Task.FromResult<Action<SmartSoftwareApplicationCreationOptions>>(options => options.UseAutofac());
    }
}
