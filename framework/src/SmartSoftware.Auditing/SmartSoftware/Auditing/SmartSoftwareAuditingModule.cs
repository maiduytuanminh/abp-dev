using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Data;
using SmartSoftware.Json;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Security;
using SmartSoftware.Threading;
using SmartSoftware.Timing;

namespace SmartSoftware.Auditing;

[DependsOn(
    typeof(SmartSoftwareDataModule),
    typeof(SmartSoftwareJsonModule),
    typeof(SmartSoftwareTimingModule),
    typeof(SmartSoftwareSecurityModule),
    typeof(SmartSoftwareThreadingModule),
    typeof(SmartSoftwareMultiTenancyModule),
    typeof(SmartSoftwareAuditingContractsModule)
    )]
public class SmartSoftwareAuditingModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(AuditingInterceptorRegistrar.RegisterIfNeeded);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var applicationName = context.Services.GetApplicationName();
        
        if (!applicationName.IsNullOrEmpty())
        {
            Configure<SmartSoftwareAuditingOptions>(options =>
            {
                options.ApplicationName = applicationName;
            });
        }
    }
}
