using SmartSoftware.AuditLogging.EntityFrameworkCore;
using SmartSoftware.Modularity;

namespace SmartSoftware.AuditLogging;

[DependsOn(
    typeof(SmartSoftwareAuditLoggingEntityFrameworkCoreTestModule)
    )]
public class SmartSoftwareAuditLoggingTestModule : SmartSoftwareModule
{

}
