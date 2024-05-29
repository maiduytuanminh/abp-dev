using SmartSoftwarePerfTest.WithSmartSoftware.Entities;
using Microsoft.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;

namespace SmartSoftwarePerfTest.WithSmartSoftware.EntityFramework
{
    public class BookDbContext : SmartSoftwareDbContext<BookDbContext>
    {
        public DbSet<Book> Books { get; set; }

        public BookDbContext(DbContextOptions<BookDbContext> builderOptions)
            : base(builderOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(b =>
            {
                b.ToTable("Books");
                b.Property(x => x.Name).HasMaxLength(128);
            });
        }
    }
}
