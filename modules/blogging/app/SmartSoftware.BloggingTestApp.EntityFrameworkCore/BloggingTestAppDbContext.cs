using Microsoft.EntityFrameworkCore;
using SmartSoftware.BlobStoring.Database.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.SettingManagement.EntityFrameworkCore;
using SmartSoftware.Blogging.EntityFrameworkCore;

namespace SmartSoftware.BloggingTestApp.EntityFrameworkCore
{
    public class BloggingTestAppDbContext : SmartSoftwareDbContext<BloggingTestAppDbContext>
    {
        public BloggingTestAppDbContext(DbContextOptions<BloggingTestAppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigurePermissionManagement();
            modelBuilder.ConfigureSettingManagement();
            modelBuilder.ConfigureIdentity();
            modelBuilder.ConfigureBlogging();
            modelBuilder.ConfigureBlobStoring();
        }
    }
}
