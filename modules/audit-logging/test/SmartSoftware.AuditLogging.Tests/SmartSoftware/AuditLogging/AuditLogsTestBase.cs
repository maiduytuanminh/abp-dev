using System;
using System.Collections.Generic;
using System.Linq;
using SmartSoftware.AuditLogging.EntityFrameworkCore;

namespace SmartSoftware.AuditLogging;

public class AuditLogsTestBase : AuditLoggingTestBase<SmartSoftwareAuditLoggingTestModule>
{
    protected virtual void UsingDbContext(Action<IAuditLoggingDbContext> action)
    {
        using (var dbContext = GetRequiredService<IAuditLoggingDbContext>())
        {
            action.Invoke(dbContext);
        }
    }

    protected virtual T UsingDbContext<T>(Func<IAuditLoggingDbContext, T> action)
    {
        using (var dbContext = GetRequiredService<IAuditLoggingDbContext>())
        {
            return action.Invoke(dbContext);
        }
    }

    protected List<AuditLog> GetAuditLogsFromDbContext()
    {
        return UsingDbContext(context =>
            context.AuditLogs.IncludeDetails().ToList()
        );
    }
}
