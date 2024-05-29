using System;
using SmartSoftware.TenantManagement.EntityFrameworkCore;

namespace SmartSoftware.TenantManagement;

public abstract class SmartSoftwareTenantManagementApplicationTestBase : TenantManagementTestBase<SmartSoftwareTenantManagementApplicationTestModule>
{
    protected virtual void UsingDbContext(Action<ITenantManagementDbContext> action)
    {
        using (var dbContext = GetRequiredService<ITenantManagementDbContext>())
        {
            action.Invoke(dbContext);
        }
    }

    protected virtual T UsingDbContext<T>(Func<ITenantManagementDbContext, T> action)
    {
        using (var dbContext = GetRequiredService<ITenantManagementDbContext>())
        {
            return action.Invoke(dbContext);
        }
    }
}
