using Microsoft.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Modeling;

namespace SmartSoftware.EntityFrameworkCore.TestApp.SecondContext;

public class SecondDbContext : SmartSoftwareDbContext<SecondDbContext>
{
    public DbSet<BookInSecondDbContext> Books { get; set; }

    public DbSet<PhoneInSecondDbContext> Phones { get; set; }

    public SecondDbContext(DbContextOptions<SecondDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PhoneInSecondDbContext>(b =>
        {
            b.HasKey(p => new { p.PersonId, p.Number });
        });
    }
}
