using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;

namespace SmartSoftware.Identity.EntityFrameworkCore;

public class LazyLoading_Tests : LazyLoading_Tests<SmartSoftwareIdentityEntityFrameworkCoreTestModule>
{
    protected override void BeforeAddApplication(IServiceCollection services)
    {
        services.Configure<SmartSoftwareDbContextOptions>(options =>
        {
            options.PreConfigure<IdentityDbContext>(context =>
            {
                context.DbContextOptions.UseLazyLoadingProxies();
            });
        });
    }
}
