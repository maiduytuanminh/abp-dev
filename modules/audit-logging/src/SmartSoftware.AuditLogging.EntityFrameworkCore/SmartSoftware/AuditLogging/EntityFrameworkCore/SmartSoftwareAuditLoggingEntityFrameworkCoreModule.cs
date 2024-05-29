using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Modularity;

namespace SmartSoftware.AuditLogging.EntityFrameworkCore;

[DependsOn(typeof(SmartSoftwareAuditLoggingDomainModule))]
[DependsOn(typeof(SmartSoftwareEntityFrameworkCoreModule))]
public class SmartSoftwareAuditLoggingEntityFrameworkCoreModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSmartSoftwareDbContext<SmartSoftwareAuditLoggingDbContext>(options =>
        {
            options.AddRepository<AuditLog, EfCoreAuditLogRepository>();
        });
    }
}
