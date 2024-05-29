using System.Threading.Tasks;
using SmartSoftware.Auditing;

namespace SmartSoftware.AuditLogging;

public interface IAuditLogInfoToAuditLogConverter
{
    Task<AuditLog> ConvertAsync(AuditLogInfo auditLogInfo);
}
