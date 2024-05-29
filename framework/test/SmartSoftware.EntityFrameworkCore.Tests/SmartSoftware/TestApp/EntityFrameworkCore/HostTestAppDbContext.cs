using Microsoft.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.TestApp.FifthContext;

namespace SmartSoftware.TestApp.EntityFrameworkCore;

public class HostTestAppDbContext : SmartSoftwareDbContext<HostTestAppDbContext>, IFifthDbContext
{
    public DbSet<FifthDbContextDummyEntity> FifthDbContextDummyEntity { get; set; }
    public DbSet<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity { get; set; }

    public HostTestAppDbContext(DbContextOptions<HostTestAppDbContext> options)
        : base(options)
    {
    }
}
