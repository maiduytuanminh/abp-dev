using Microsoft.EntityFrameworkCore;
using SmartSoftware.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.TestApp.FifthContext;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.TestApp.EntityFrameworkCore;

[ReplaceDbContext(typeof(IFifthDbContext), MultiTenancySides.Tenant)]
public class TenantTestAppDbContext : SmartSoftwareDbContext<TenantTestAppDbContext>, IFifthDbContext
{
    public DbSet<FifthDbContextDummyEntity> FifthDbContextDummyEntity { get; set; }
    public DbSet<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity { get; set; }

    public TenantTestAppDbContext(DbContextOptions<TenantTestAppDbContext> options)
        : base(options)
    {
    }
}
