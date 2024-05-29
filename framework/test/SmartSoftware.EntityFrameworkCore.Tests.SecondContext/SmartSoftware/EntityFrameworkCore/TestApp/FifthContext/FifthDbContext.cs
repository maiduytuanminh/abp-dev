using Microsoft.EntityFrameworkCore;

namespace SmartSoftware.EntityFrameworkCore.TestApp.FifthContext;

public class FifthDbContext : SmartSoftwareDbContext<FifthDbContext>, IFifthDbContext
{
    public DbSet<FifthDbContextDummyEntity> FifthDbContextDummyEntity { get; set; }

    public DbSet<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity { get; set; }

    public FifthDbContext(DbContextOptions<FifthDbContext> options)
        : base(options)
    {
    }
}
