using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;

namespace SmartSoftware.AuditLogging.MongoDB;

[ConnectionStringName(SmartSoftwareAuditLoggingDbProperties.ConnectionStringName)]
public class AuditLoggingMongoDbContext : SmartSoftwareMongoDbContext, IAuditLoggingMongoDbContext
{
    public IMongoCollection<AuditLog> AuditLogs => Collection<AuditLog>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureAuditLogging();
    }
}
