namespace SmartSoftware.Auditing;

public interface IAuditLogScope
{
    AuditLogInfo Log { get; }
}
