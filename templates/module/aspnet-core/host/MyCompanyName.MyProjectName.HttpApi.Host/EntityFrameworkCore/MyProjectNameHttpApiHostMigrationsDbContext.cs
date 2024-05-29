using Microsoft.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore;

public class MyProjectNameHttpApiHostMigrationsDbContext : SmartSoftwareDbContext<MyProjectNameHttpApiHostMigrationsDbContext>
{
    public MyProjectNameHttpApiHostMigrationsDbContext(DbContextOptions<MyProjectNameHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureMyProjectName();
    }
}
