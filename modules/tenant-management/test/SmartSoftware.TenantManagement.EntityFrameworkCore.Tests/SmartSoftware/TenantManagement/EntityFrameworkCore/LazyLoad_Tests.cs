using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;

namespace SmartSoftware.TenantManagement.EntityFrameworkCore;

public class LazyLoad_Tests : LazyLoad_Tests<SmartSoftwareTenantManagementEntityFrameworkCoreTestModule>
{
    protected override void BeforeAddApplication(IServiceCollection services)
    {
        services.Configure<SmartSoftwareDbContextOptions>(options =>
        {
            options.PreConfigure<TenantManagementDbContext>(context =>
            {
                context.DbContextOptions.UseLazyLoadingProxies();
            });
        });
    }
}
