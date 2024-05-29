using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;

namespace SmartSoftware.AuditLogging.MongoDB;

[DependsOn(typeof(SmartSoftwareAuditLoggingDomainModule))]
[DependsOn(typeof(SmartSoftwareMongoDbModule))]
public class SmartSoftwareAuditLoggingMongoDbModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<AuditLoggingMongoDbContext>(options =>
        {
            options.AddRepository<AuditLog, MongoAuditLogRepository>();
        });
    }
}
