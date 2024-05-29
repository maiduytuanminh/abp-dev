using Microsoft.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.SettingManagement.EntityFrameworkCore;
using SmartSoftware.Docs.EntityFrameworkCore;

namespace SmartSoftwareDocs.EntityFrameworkCore
{
    public class SmartSoftwareDocsDbContext : SmartSoftwareDbContext<SmartSoftwareDocsDbContext>
    {
        public SmartSoftwareDocsDbContext(DbContextOptions<SmartSoftwareDocsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigurePermissionManagement();
            modelBuilder.ConfigureSettingManagement();
            modelBuilder.ConfigureIdentity();
            modelBuilder.ConfigureDocs();
        }
    }
}
