using Microsoft.EntityFrameworkCore;
using SmartSoftware.AuditLogging.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.IdentityServer.EntityFrameworkCore;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.SettingManagement.EntityFrameworkCore;
using SmartSoftware.TenantManagement.EntityFrameworkCore;

namespace SmartSoftware.CmsKit.EntityFrameworkCore;

public class IdentityServerHostMigrationsDbContext : SmartSoftwareDbContext<IdentityServerHostMigrationsDbContext>
{
    public IdentityServerHostMigrationsDbContext(DbContextOptions<IdentityServerHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigureIdentity();
        modelBuilder.ConfigureIdentityServer();
        modelBuilder.ConfigureTenantManagement();
    }
}
