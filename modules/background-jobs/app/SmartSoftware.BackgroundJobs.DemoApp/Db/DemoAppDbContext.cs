using Microsoft.EntityFrameworkCore;
using SmartSoftware.BackgroundJobs.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;

namespace SmartSoftware.BackgroundJobs.DemoApp.Db;

public class DemoAppDbContext : SmartSoftwareDbContext<DemoAppDbContext>
{
    public DemoAppDbContext(DbContextOptions<DemoAppDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureBackgroundJobs();
    }
}
