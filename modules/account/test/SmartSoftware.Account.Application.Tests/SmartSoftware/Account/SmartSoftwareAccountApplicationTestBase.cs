using System;
using System.Linq;
using SmartSoftware.Domain.Entities;
using SmartSoftware.Identity;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.Testing;

namespace SmartSoftware.Account;

public class SmartSoftwareAccountApplicationTestBase : SmartSoftwareIntegratedTest<SmartSoftwareAccountApplicationTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    protected virtual IdentityUser GetUser(string userName)
    {
        var user = UsingDbContext(context => context.Users.FirstOrDefault(u => u.UserName == userName));
        if (user == null)
        {
            throw new EntityNotFoundException();
        }

        return user;
    }

    protected virtual T UsingDbContext<T>(Func<IIdentityDbContext, T> action)
    {
        using (var dbContext = GetRequiredService<IIdentityDbContext>())
        {
            return action.Invoke(dbContext);
        }
    }
}
