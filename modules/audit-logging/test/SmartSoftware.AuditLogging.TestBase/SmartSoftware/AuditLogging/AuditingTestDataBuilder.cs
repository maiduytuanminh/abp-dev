using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AuditLogging;

public class AuditingTestDataBuilder : ITransientDependency
{
    private readonly IAuditLogRepository _auditLogRepository;

    public AuditingTestDataBuilder(IAuditLogRepository auditLogRepository)
    {
        _auditLogRepository = auditLogRepository;
    }

    public void Build()
    {

    }
}
