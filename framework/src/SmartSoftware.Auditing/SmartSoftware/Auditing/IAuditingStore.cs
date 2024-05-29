using System.Threading.Tasks;

namespace SmartSoftware.Auditing;

public interface IAuditingStore
{
    Task SaveAsync(AuditLogInfo auditInfo);
}
