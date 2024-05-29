using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;

namespace SmartSoftware.AuditLogging.MongoDB;

[ConnectionStringName(SmartSoftwareAuditLoggingDbProperties.ConnectionStringName)]
public interface IAuditLoggingMongoDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<AuditLog> AuditLogs { get; }
}
