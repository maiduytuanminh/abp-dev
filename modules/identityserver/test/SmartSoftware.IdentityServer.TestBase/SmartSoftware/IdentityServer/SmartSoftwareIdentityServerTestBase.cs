using System;
using System.Collections.Generic;
using System.Text;
using SmartSoftware.Modularity;
using SmartSoftware.Testing;

namespace SmartSoftware.IdentityServer;

public class SmartSoftwareIdentityServerTestBase<TStartupModule> : SmartSoftwareIntegratedTest<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
