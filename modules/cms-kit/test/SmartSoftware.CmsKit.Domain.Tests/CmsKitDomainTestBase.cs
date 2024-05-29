using System;
using SmartSoftware.CmsKit.EntityFrameworkCore;

namespace SmartSoftware.CmsKit;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class CmsKitDomainTestBase : CmsKitTestBase<CmsKitDomainTestModule>
{
    protected virtual void UsingDbContext(Action<ICmsKitDbContext> action)
    {
        using (var dbContext = GetRequiredService<ICmsKitDbContext>())
        {
            action.Invoke(dbContext);
        }
    }

    protected virtual T UsingDbContext<T>(Func<ICmsKitDbContext, T> action)
    {
        using (var dbContext = GetRequiredService<ICmsKitDbContext>())
        {
            return action.Invoke(dbContext);
        }
    }
}
